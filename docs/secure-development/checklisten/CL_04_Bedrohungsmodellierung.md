<!--
Quelle / Source: generische Ausbildungs- und Pruefgrundlage, bereinigt am 2026-06-17.
Dieses Dokument ist organisationsneutral und als generische Ausbildungs- und Pruefgrundlage formuliert.
Source: generic training and review baseline, generalized on 2026-06-17.
This document is organization-neutral and written as a generic training and review baseline.
-->

> **DE:** Diese Checkliste ist generisch und projektunabhaengig. Sie ist als Ausbildungs-, Review- und Haertungsgrundlage gedacht. Eine Nichtanwendbarkeit muss als `N/A` mit kurzer Begruendung dokumentiert werden.
>
> **EN:** This checklist is generic and project-independent. It is intended as a training, review, and hardening baseline. Non-applicability must be documented as `N/A` with a short rationale.

## Checkliste 04 – Bedrohungsmodellierung / Threat Modeling

**Dokument-ID / Document ID:** CL-04
**Version / Version:** 2.0.0
**Baseline-Version / Baseline version:** 3.0.0
**Dokumenttyp / Document type:** Kanonische, wiederverwendbare Pruefvorlage / Canonical reusable review template

### Nachweisinstanz / Evidence Instance

Diese Datei ist eine wiederverwendbare Vorlage. Ausgefüllte Projektnachweise werden unter `docs/security/secure-development/<datum>-<scope>/` abgelegt und nennen Projekt, Scope, Prüfdatum, Baseline-Version, verantwortliche Person und Reviewer. / This file is a reusable template. Completed project evidence is stored under `docs/security/secure-development/<date>-<scope>/` and names project, scope, review date, baseline version, responsible person, and reviewer.

### Zweck / Purpose

**DE:** Diese Checkliste sichert ein vollständiges, nachvollziehbares
Bedrohungsmodell auf STRIDE-Basis mit CIA-Bewertung und CAPEC-Bezug. Sie
ist mitgeltend zur Richtlinie „Sichere Entwicklung".

**EN:** This checklist ensures a complete, traceable threat model based on
STRIDE, with CIA rating and CAPEC references. It accompanies the guideline
"Secure Development".

### Geltungsbereich / Scope

**DE:** Pflicht für jedes Vorhaben mit eigener Architektur. Aktualisierung
bei wesentlichen Änderungen (neue Schnittstelle, neue Datenklasse, neuer
Auftragsverarbeiter, neue Vertrauensgrenze).

**EN:** Mandatory for every project with its own architecture. Update on
material changes (new interface, new data class, new processor, new trust
boundary).

### Mitgeltende Dokumente / Related Documents

- Richtlinie Sichere Entwicklung
- ISO/IEC 27002:2022 A.8.27, A.8.28
- STRIDE (Microsoft Threat Modeling)
- CAPEC – Common Attack Pattern Enumeration and Classification
- arc42 Section 8

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

**DE:** Ein Upload-Endpunkt verarbeitet Dateien von externen Personen. Das DFD zeigt Browser, API, Virenscanner, Dateispeicher und Datenbank. STRIDE prueft Spoofing, Tampering und Information Disclosure; CAPEC verweist auf passende Upload- und Injection-Muster.

**EN:** An upload endpoint processes files from external users. The DFD shows browser, API, virus scanner, file storage, and database. STRIDE checks spoofing, tampering, and information disclosure; CAPEC points to matching upload and injection patterns.

### A11Y-Hinweise / A11Y Notes

**DE:** Beim Ausfüllen dieser Checkliste müssen alle Nachweise auch textlich verständlich sein. Verweise sollen beschreibende Linktexte haben. Screenshots, Diagramme oder Scan-Auszüge brauchen eine kurze Textbeschreibung. Der Status darf nicht nur über Farbe erkennbar sein.

**EN:** When this checklist is filled in, all evidence must also be understandable as text. References should use descriptive link text. Screenshots, diagrams, or scan extracts need a short text description. The status must not be shown by color alone.

### Wichtige Begriffe / Key Terms

**DE:** Die folgenden Begriffe kommen in dieser Checkliste vor. Die Links springen zum Glossar dieses Kapitels, damit Auszubildende und Entwickler:innen ohne Sicherheits-Spezialwissen die Begriffe direkt nachlesen können.

**EN:** The following terms appear in this checklist. The links jump to this chapter's glossary so that apprentices and developers without specialist security knowledge can look them up directly.

- [Bedrohungsmodell / Threat Model](#cl-04-glossar-threat-model)
- [Asset / Schutzwert](#cl-04-glossar-asset)
- [Vertrauensgrenze / Trust Boundary](#cl-04-glossar-trust-boundary)
- [Datenfluss / Data Flow](#cl-04-glossar-data-flow)
- [STRIDE](#cl-04-glossar-stride)
- [CAPEC](#cl-04-glossar-capec)
- [Risikobewertung / Risk Rating](#cl-04-glossar-risk-rating)
- [Maßnahme / Mitigation](#cl-04-glossar-mitigation)
- [Evidenz / Evidence](#cl-04-glossar-evidenz)
- [Audit](#cl-04-glossar-audit)

### Checkliste / Checklist

#### CL-04-01: Werte und Schutzbedarf / Assets and Protection Need

- **DE:** Eine Asset-Liste benennt alle schützenswerten Werte: personen-
  bezogene Daten (PII), Geschäftsgeheimnisse, Authentifizierungs-
  Geheimnisse (API-Keys, Passwort-Hashes, Session-Tokens), kryptografische
  Schlüssel, Geschäftsprozesse, Verfügbarkeit kritischer Endpunkte,
  Quellcode mit kompetitivem Vorteil. Für jedes Asset wird die CIA-
  Matrix mit Schutzbedarf-Klassen ausgefüllt: Vertraulichkeit
  (öffentlich, intern, vertraulich, streng vertraulich), Integrität
  (Manipulation tolerierbar, geschäftsstörend, gefährdend),
  Verfügbarkeit (Ausfall < 1 Tag, < 4 Stunden, < 1 Stunde). Beispiele
  für Klassifikations-Schemata: BSI Schutzbedarfsfeststellung
  (normal, hoch, sehr hoch), ISO/IEC 27001 Annex A.5.12, Microsoft
  Data Classification (Public, General, Confidential, Highly
  Confidential), TLP (Traffic Light Protocol). Werkzeuge:
  Tabellenkalkulation oder strukturiertes YAML/JSON im Repo;
  pytm-Threat-Model-Code mit `Asset()`-Klassen; Microsoft Threat
  Modeling Tool mit Asset-Properties. Pro Asset wird der Owner
  benannt — die Person oder Rolle, die für Schutz verantwortlich ist.
- **EN:** An asset list names all values worth protecting: personally
  identifiable information (PII), trade secrets, authentication
  secrets (API keys, password hashes, session tokens), cryptographic
  keys, business processes, availability of critical endpoints,
  source code with competitive advantage. For each asset, the CIA
  matrix is filled with protection-need classes: confidentiality
  (public, internal, confidential, strictly confidential), integrity
  (tampering tolerable, business-disruptive, dangerous),
  availability (downtime < 1 day, < 4 hours, < 1 hour). Examples of
  classification schemes: BSI protection-need assessment (normal,
  high, very high), ISO/IEC 27001 Annex A.5.12, Microsoft Data
  Classification (Public, General, Confidential, Highly
  Confidential), TLP (Traffic Light Protocol). Tooling: spreadsheet
  or structured YAML/JSON in the repo; pytm threat-model code with
  `Asset()` classes; Microsoft Threat Modeling Tool with asset
  properties. Per asset, an owner is named — the person or role
  responsible for protection.
- **Akzeptanz / Acceptance:** Asset-Liste mit Klassifikations-Schema,
  CIA-Bewertung und Owner je Asset. / Asset list with classification
  scheme, CIA rating, and owner per asset.
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

#### CL-04-02: Vertrauensgrenzen und Datenflüsse / Trust Boundaries and Data Flows

- **DE:** Ein Datenflussdiagramm (DFD) zeigt fünf Element-Typen:
  **External Entity** (Akteur, z. B. Browser-Nutzer, Drittsystem),
  **Process** (Verarbeitungseinheit, z. B. Microservice, API-Endpunkt),
  **Data Store** (Datenbank, Cache, Datei, Queue), **Data Flow**
  (Pfeil zwischen Elementen mit Protokoll und Inhalt), **Trust
  Boundary** (gestrichelte Linie zwischen Vertrauensbereichen). Jede
  Trust Boundary trägt eine kurze Begründung: „HTTPS-Eingang aus
  Internet", „mTLS zwischen Service A und B", „Datenbank-Verbindung
  mit Service-Account". Werkzeuge: Microsoft Threat Modeling Tool
  (kostenlos, Windows-only, mit STRIDE-Templates); OWASP Threat
  Dragon (Web/Electron, Open Source); pytm (Python, Code-as-Threat-
  Model mit textbasiertem DSL); IriusRisk (kommerziell);
  draw.io/diagrams.net mit STRIDE-Vorlagen; Mermaid `flowchart`-
  Diagramme im Markdown. Das DFD wird im Repo gepflegt
  (`docs/security/dfd.md` oder `docs/security/dfd.drawio`),
  Diagramm und Quelldatei werden gemeinsam versioniert.
- **EN:** A data flow diagram (DFD) shows five element types:
  **External Entity** (actor, e.g. browser user, third-party system),
  **Process** (processing unit, e.g. microservice, API endpoint),
  **Data Store** (database, cache, file, queue), **Data Flow**
  (arrow between elements with protocol and payload), **Trust
  Boundary** (dashed line between trust zones). Each trust boundary
  carries a short justification: "HTTPS ingress from Internet",
  "mTLS between service A and B", "database connection with
  service account". Tooling: Microsoft Threat Modeling Tool (free,
  Windows-only, with STRIDE templates); OWASP Threat Dragon (web/
  Electron, open source); pytm (Python, threat-model-as-code with
  text DSL); IriusRisk (commercial); draw.io/diagrams.net with
  STRIDE stencils; Mermaid `flowchart` diagrams in Markdown. The
  DFD lives in the repo (`docs/security/dfd.md` or
  `docs/security/dfd.drawio`); diagram and source file are
  versioned together.
- **Akzeptanz / Acceptance:** DFD mit allen fünf Element-Typen,
  Trust-Boundary-Begründung und versionierten Quelldateien im
  Repository. / DFD with all five element types, trust-boundary
  justification, and versioned source files in the repository.
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

#### CL-04-03: STRIDE pro Element / STRIDE per Element

- **DE:** STRIDE (Microsoft, 1999) bietet sechs Bedrohungs-Kategorien
  mit klaren Sicherheitseigenschaften, die sie verletzen:
  **Spoofing** (Identitäts-Vortäuschung) verletzt **Authentifizierung**;
  **Tampering** (Manipulation) verletzt **Integrität**;
  **Repudiation** (Abstreitbarkeit) verletzt **Nicht-Abstreitbarkeit**;
  **Information Disclosure** verletzt **Vertraulichkeit**;
  **Denial of Service** verletzt **Verfügbarkeit**;
  **Elevation of Privilege** verletzt **Autorisierung**. Pro DFD-
  Element werden die zutreffenden Buchstaben geprüft: **External
  Entity** typisch S, R; **Process** typisch S, T, R, I, D, E (alle);
  **Data Store** typisch T, R, I, D; **Data Flow** typisch T, I, D.
  Beispiel-Bedrohungen: Spoofing — Angreifer fälscht JWT mit
  schwachem HMAC-Secret (Gegenmaßnahme: starkes Secret im KMS,
  Algorithmus auf RS256/ES256); Tampering — SQL-Injection
  manipuliert Daten (Gegenmaßnahme: parametrisierte Queries);
  Information Disclosure — Stack-Trace im API-Response (Gegenmaßnahme:
  globaler Error-Handler, Production-Logging); Elevation of Privilege
  — Path-Traversal liest beliebige Dateien (Gegenmaßnahme:
  Allow-Listen, kanonische Pfad-Validierung). Werkzeuge: Microsoft
  Threat Modeling Tool generiert STRIDE-Vorschläge je Element;
  pytm `Threat()` mit STRIDE-Klassifikation.
- **EN:** STRIDE (Microsoft, 1999) provides six threat categories
  with clear security properties they violate: **Spoofing**
  (identity forgery) violates **Authentication**; **Tampering**
  (manipulation) violates **Integrity**; **Repudiation**
  (deniability) violates **Non-Repudiation**; **Information
  Disclosure** violates **Confidentiality**; **Denial of Service**
  violates **Availability**; **Elevation of Privilege** violates
  **Authorisation**. Per DFD element, the applicable letters are
  evaluated: **External Entity** typically S, R; **Process**
  typically S, T, R, I, D, E (all); **Data Store** typically T, R,
  I, D; **Data Flow** typically T, I, D. Example threats: Spoofing
  — attacker forges JWT with weak HMAC secret (mitigation: strong
  secret in KMS, algorithm to RS256/ES256); Tampering — SQL
  injection manipulates data (mitigation: parameterised queries);
  Information Disclosure — stack trace in API response
  (mitigation: global error handler, production logging);
  Elevation of Privilege — path traversal reads arbitrary files
  (mitigation: allow-lists, canonical path validation). Tooling:
  Microsoft Threat Modeling Tool generates STRIDE suggestions per
  element; pytm `Threat()` with STRIDE classification.
- **Akzeptanz / Acceptance:** STRIDE-Tabelle je DFD-Element
  vollständig ausgefüllt; je Bedrohung mindestens eine
  Gegenmaßnahme. / STRIDE table per DFD element fully filled;
  per threat at least one mitigation.
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

#### CL-04-04: CAPEC-Zuordnung / CAPEC Mapping

- **DE:** Während STRIDE die Bedrohungs-Kategorien benennt, liefert CAPEC
  konkrete Angriffsmuster — quasi „so läuft der Angriff in der Realität
  ab". Typische CAPEC-Muster pro STRIDE-Kategorie: **Spoofing** —
  CAPEC-148 Content Spoofing, CAPEC-151 Identity Spoofing, CAPEC-115
  Authentication Bypass; **Tampering** — CAPEC-66 SQL Injection,
  CAPEC-242 Code Injection, CAPEC-233 Privilege Escalation;
  **Repudiation** — CAPEC-67 String Format Overflow im Logging,
  CAPEC-93 Log Injection; **Information Disclosure** — CAPEC-118
  Collect and Analyze Information, CAPEC-37 Retrieve Embedded
  Sensitive Data; **Denial of Service** — CAPEC-125 Flooding,
  CAPEC-130 Excessive Allocation, CAPEC-227 Sustained Client
  Engagement; **Elevation of Privilege** — CAPEC-233 Privilege
  Escalation, CAPEC-22 Exploiting Trust in Client, CAPEC-94
  Adversary in the Middle. Pro Hochrisiko-Fluss werden mindestens
  drei CAPEC-IDs benannt — mit Voraussetzungen („Angreifer hat
  Netzwerkzugang"), Skill-Level (Low/Medium/High), Auswirkung und
  konkreten Gegenmaßnahmen. Werkzeuge: MITRE-CAPEC-Webseite mit
  XML-Export; Threat-Dragon und IriusRisk haben CAPEC-Bibliotheken;
  pytm-Threats können CAPEC-IDs als Tags tragen.
- **EN:** While STRIDE names threat categories, CAPEC delivers concrete
  attack patterns — essentially "how the attack actually plays out".
  Typical CAPEC patterns per STRIDE category: **Spoofing** — CAPEC-148
  Content Spoofing, CAPEC-151 Identity Spoofing, CAPEC-115
  Authentication Bypass; **Tampering** — CAPEC-66 SQL Injection,
  CAPEC-242 Code Injection, CAPEC-233 Privilege Escalation;
  **Repudiation** — CAPEC-67 String Format Overflow in logging,
  CAPEC-93 Log Injection; **Information Disclosure** — CAPEC-118
  Collect and Analyze Information, CAPEC-37 Retrieve Embedded
  Sensitive Data; **Denial of Service** — CAPEC-125 Flooding,
  CAPEC-130 Excessive Allocation, CAPEC-227 Sustained Client
  Engagement; **Elevation of Privilege** — CAPEC-233 Privilege
  Escalation, CAPEC-22 Exploiting Trust in Client, CAPEC-94
  Adversary in the Middle. Per high-risk flow, at least three CAPEC
  IDs are named — with preconditions ("attacker has network
  access"), skill level (low/medium/high), impact, and concrete
  mitigations. Tooling: MITRE CAPEC website with XML export;
  Threat Dragon and IriusRisk have CAPEC libraries; pytm threats
  can carry CAPEC IDs as tags.
- **Akzeptanz / Acceptance:** Mindestens drei CAPEC-IDs je Hochrisiko-
  Fluss mit Voraussetzungen, Skill-Level, Auswirkung und
  Gegenmaßnahme. / At least three CAPEC IDs per high-risk flow with
  preconditions, skill level, impact, and mitigation.
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

#### CL-04-05: Risikobewertung / Risk Rating

- **DE:** Jede Bedrohung wird nach Eintrittswahrscheinlichkeit
  (Likelihood) und Auswirkung (Impact) bewertet. Empfohlene Schemata:
  **DREAD** (Damage, Reproducibility, Exploitability, Affected Users,
  Discoverability — Skala 1–10) — einfach, aber subjektiv; **CVSS v3.1
  / v4.0** (Common Vulnerability Scoring System) — objektive Metriken
  für Attack Vector, Complexity, Privileges, User Interaction, Scope,
  CIA-Impact (0,0–10,0); **OWASP Risk Rating Methodology** —
  unterteilt Likelihood (Threat Agent + Vulnerability) und Impact
  (Technical + Business); **NIST SP 800-30** mit qualitativen Stufen
  (Very Low–Very High). Für agile Teams reicht oft eine 3×3- oder
  5×5-Matrix mit niedrig/mittel/hoch je Achse, kombiniert zu kritisch/
  hoch/mittel/niedrig. Pro Bedrohung wird die Begründung in zwei bis
  drei Sätzen festgehalten (Voraussetzungen, technische Details,
  bestehende Schutzschichten). Werkzeuge: CVSS-Online-Calculator
  von FIRST (`https://www.first.org/cvss/calculator/4.0`);
  Threat-Dragon-Risk-Editor; pytm berechnet Risiko aus Likelihood
  und Impact-Tags.
- **EN:** Each threat is rated by likelihood and impact. Recommended
  schemes: **DREAD** (Damage, Reproducibility, Exploitability,
  Affected Users, Discoverability — scale 1–10) — simple but
  subjective; **CVSS v3.1 / v4.0** (Common Vulnerability Scoring
  System) — objective metrics for Attack Vector, Complexity,
  Privileges, User Interaction, Scope, CIA impact (0.0–10.0);
  **OWASP Risk Rating Methodology** — splits likelihood (threat
  agent + vulnerability) and impact (technical + business);
  **NIST SP 800-30** with qualitative tiers (Very Low–Very High).
  For agile teams, a 3×3 or 5×5 matrix with low/medium/high per
  axis is often enough, combined to critical/high/medium/low. Per
  threat, the justification is captured in two to three sentences
  (preconditions, technical details, existing protection layers).
  Tooling: FIRST CVSS online calculator
  (`https://www.first.org/cvss/calculator/4.0`); Threat Dragon
  risk editor; pytm computes risk from likelihood and impact tags.
- **Akzeptanz / Acceptance:** Risikomatrix mit gewähltem Schema,
  Likelihood- und Impact-Skala und Begründung je Bedrohung. / Risk
  matrix with chosen scheme, likelihood and impact scale, and
  justification per threat.
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

#### CL-04-06: Maßnahmen / Mitigations

- **DE:** Risikobehandlung folgt vier Strategien (ISO/IEC 27005,
  NIST SP 800-30): **Vermeiden** — die riskante Funktion wird gestrichen
  (z. B. „kein File-Upload" statt „File-Upload mit Hardening");
  **Vermindern** — durch technische Maßnahmen (Validierung,
  Verschlüsselung, Rate-Limiting, WAF, MFA) oder organisatorische
  Maßnahmen (Berechtigungs-Reviews, Trainings); **Übertragen** —
  durch Versicherung, Outsourcing an spezialisierten Dienstleister,
  oder Cloud-Provider mit SLA; **Akzeptieren** — Risiko wird
  bewusst getragen, dokumentiert mit Begründung, Ablaufdatum,
  Reviewer und Eskalations-Pfad. Pro Bedrohung wird die gewählte
  Strategie und mindestens eine konkrete Maßnahme benannt; Maßnahmen
  verlinken auf S-ADRs, User Stories oder Konfigurations-Dateien.
  Beispiel: SQL-Injection (CAPEC-66) → vermindern durch
  parametrisierte Queries (PreparedStatement, ORM) + WAF + DB-User
  ohne `DROP`-Recht. Risiko-Akzeptanzen brauchen die Unterschrift
  einer befugten Person (Product Owner, Security Lead, Risk Manager) und
  ein Ablaufdatum, nach dem die Akzeptanz neu bewertet wird.
- **EN:** Risk treatment follows four strategies (ISO/IEC 27005,
  NIST SP 800-30): **Avoid** — the risky function is removed (e.g.
  "no file upload" rather than "file upload with hardening");
  **Reduce** — via technical controls (validation, encryption, rate
  limiting, WAF, MFA) or organisational controls (permission
  reviews, training); **Transfer** — via insurance, outsourcing to
  a specialised provider, or cloud provider SLA; **Accept** — risk
  is deliberately carried, documented with justification, expiry
  date, reviewer, and escalation path. Per threat, the chosen
  strategy and at least one concrete mitigation are named;
  mitigations link to S-ADRs, user stories, or configuration files.
  Example: SQL injection (CAPEC-66) → reduce via parameterised
  queries (PreparedStatement, ORM) + WAF + DB user without `DROP`
  privilege. Risk acceptances require the signature of an
  authorised person (Product Owner, Security Lead, Risk Manager) and an
  expiry date after which the acceptance is re-evaluated.
- **Akzeptanz / Acceptance:** Maßnahme je Bedrohung mit Strategie,
  Verlinkung zu S-ADR/Story/Config; Risiko-Akzeptanzen mit
  Unterschrift und Ablaufdatum. / Mitigation per threat with
  strategy and link to S-ADR/story/config; risk acceptances signed
  with expiry date.
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

#### CL-04-07: Verbindung zu Anforderungen / Link to Requirements

- **DE:** Bedrohungsmodelle ohne Verbindung zu Anforderungen verstauben
  schnell. Jede Maßnahme verlinkt direkt auf ein Artefakt im
  Issue-Tracker oder Repo: GitHub-Issue/PR-URL, Jira-Ticket-ID,
  Git-Issue, Azure-DevOps-Work-Item, Linear-Ticket, S-ADR-Pfad,
  User-Story im Backlog. Beispiel-Verlinkung im Bedrohungsmodell:
  „Threat T-12: SQL Injection im Suchfeld → Mitigation: PreparedStatement
  in `SearchRepository.findByTerm()` → Story APP-456, S-ADR-0007,
  Test `SearchRepositoryTest.preventsInjection()`". Werkzeuge:
  Bidirektionale Links — Bedrohungsmodell verweist auf Issue,
  Issue-Beschreibung verweist zurück auf den Threat-ID; pytm kann
  Mitigationen mit URLs annotieren; Threat Dragon hat ein
  Mitigation-Feld; CI-Linter (z. B. eigene `verify-threat-links.sh`)
  prüft, ob alle Threat-IDs einen offenen oder geschlossenen Bezug
  haben. Beim Schließen einer Story wird der Status der zugehörigen
  Threat-ID im Bedrohungsmodell aktualisiert.
- **EN:** Threat models without a link to requirements quickly become
  shelf-ware. Each mitigation links directly to an artefact in the
  issue tracker or repo: GitHub issue/PR URL, Jira ticket ID,
  Git issue, Azure DevOps work item, Linear ticket, S-ADR path,
  user story in the backlog. Example linkage in the threat model:
  "Threat T-12: SQL injection in the search field → mitigation:
  PreparedStatement in `SearchRepository.findByTerm()` → story
  APP-456, S-ADR-0007, test `SearchRepositoryTest.preventsInjection()`".
  Tooling: bidirectional links — threat model points to the issue,
  the issue description links back to the threat ID; pytm can
  annotate mitigations with URLs; Threat Dragon has a mitigation
  field; a CI linter (e.g. a custom `verify-threat-links.sh`)
  checks that every threat ID has an open or closed reference. On
  story completion, the status of the matching threat ID in the
  threat model is updated.
- **Akzeptanz / Acceptance:** Pro Maßnahme mindestens ein
  bidirektionaler Link zu Issue, Story, S-ADR oder Test;
  Status-Sync dokumentiert. / Per mitigation, at least one
  bidirectional link to issue, story, S-ADR, or test; status sync
  documented.
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

#### CL-04-08: Aktualisierungspflicht / Update Obligation

- **DE:** Das Bedrohungsmodell wird bei jeder wesentlichen Änderung geprüft
  und bei Bedarf aktualisiert. Datum und Auslöser sind festgehalten.
  Konkrete Auslöser-Ereignisse (Trigger):
  - Architekturänderung: neue Komponente, neue Schnittstelle, neuer Datenfluss,
    neue Vertrauensgrenze, neuer Auftragsverarbeiter, neuer Cloud-Provider,
    neuer Subdienstleister oder neue providergebundene Deployment-Abhängigkeit.
  - Neue Datenklasse: zusätzliche personenbezogene Daten, Geheimnisse,
    Schlüsselmaterial oder Daten mit höherer Schutzbedarfsstufe.
  - Neue Bedrohungslage: relevante CVE, neue CAPEC-Pattern, neue Angreifergruppe,
    BSI-Lagebericht, MITRE ATT&CK Updates.
  - Vorfall (Incident): nach Sicherheitsvorfall, Lessons-Learned-Review,
    Penetrationstest-Ergebnissen oder Audit-Findings.
  - Compliance-Wechsel: neue Regulierung oder neuer Scope (CRA, NIS2, EU AI
    Act, DORA, DSGVO-Update), geänderte interne Richtlinien, BSI-C3A- oder
    BSI-C5-Relevanz.
  - Zeitlich: mindestens halbjährliche oder jährliche Routineüberprüfung.
  Werkzeugunterstützung:
  - Versionierung im Git-Repo mit Semantic Versioning (`v1.0.0` → `v1.1.0` bei
    Erweiterung, `v2.0.0` bei Architekturwechsel) und `CHANGELOG.md`.
  - GitHub Actions / Git CI Workflow `threat-model-reminder.yml`, das auf
    PRs mit Pfaden wie `arc42/`, `docs/architecture/`, `**/*.proto` oder
    `infrastructure/` automatisch einen Kommentar setzt: „Bedrohungsmodell prüfen!".
  - Tabelle „Versionshistorie / Change Log" am Ende des Bedrohungsmodells:
    Version, Datum, Auslöser, Änderung, Autor.
  - Cron-Erinnerung über Issue-Bot (z. B. `actions/github-script` mit Schedule
    `0 9 1 */6 *`) für halbjährliche Routineprüfung.
- **EN:** The threat model is reviewed on every material change and updated
  when needed. The date and trigger are recorded.
  Concrete trigger events:
  - Architecture change: new component, new interface, new data flow, new
    trust boundary, new processor, new cloud provider, new subprocessor, or
    new provider-dependent deployment dependency.
  - New data class: additional personal data, secrets, key material, or data
    with a higher protection class.
  - New threat landscape: relevant CVE, new CAPEC patterns, new threat actor,
    BSI threat report, MITRE ATT&CK updates.
  - Incident: after a security incident, lessons-learned review, pentest
    findings, or audit findings.
  - Compliance change: new regulation or new scope (CRA, NIS2, EU AI Act,
    DORA, GDPR update), changed internal policy, BSI C3A relevance, or BSI
    C5 relevance.
  - Time-based: at least every six months or annual routine review.
  Tool support:
  - Git versioning with semantic versioning (`v1.0.0` → `v1.1.0` for extension,
    `v2.0.0` for architecture change) and a `CHANGELOG.md`.
  - GitHub Actions / Git CI workflow `threat-model-reminder.yml` that posts
    an automatic PR comment when paths like `arc42/`, `docs/architecture/`,
    `**/*.proto`, or `infrastructure/` are touched: "Review threat model!".
  - "Version History / Change Log" table at the end of the threat model:
    version, date, trigger, change, author.
  - Cron reminder via issue bot (e.g. `actions/github-script` with schedule
    `0 9 1 */6 *`) for semi-annual routine review.
- **Akzeptanz / Acceptance:** Versions- oder Änderungstabelle mit Spalten
  Version, Datum, Auslöser, Änderung, Autor vorhanden; letzte Prüfung nicht
  älter als sechs Monate; CI-Reminder oder Cron-Job konfiguriert; Cloud-,
  Provider- und regulatorische Trigger sind als mögliche Auslöser benannt. /
  Version or change table with columns for version, date, trigger, change,
  and author exists; latest review is not older than six months; CI reminder
  or cron job is configured; cloud, provider, and regulatory triggers are
  named as possible triggers.
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

#### CL-04-09: Review / Review

- **DE:** Mindestens eine zweite Person (z. B. Security-Architekt, Senior
  Engineer) hat das Modell geprüft und freigegeben.
  Geeignete Reviewer-Rollen:
  - Security-Architekt:in (CISSP, ISO 27001 Lead Implementer, iSAQB CPSA-A).
  - Senior Engineer mit Domänenkenntnis und Erfahrung in STRIDE/CAPEC.
  - Externer Reviewer bei Hochrisiko-Systemen (Penetrationstester:in,
    BSI-zertifizierte:r Auditor:in).
  - Datenschutzbeauftragte:r (DSB) bei personenbezogenen Daten (DSGVO Art. 35
    DSFA).
  Review-Methoden und -Werkzeuge:
  - Pull-Request-Review im Git-Repo: Reviewer kommentiert in `threat-model.md`
    direkt an Stellen, fordert Änderungen an oder genehmigt mit Approve.
  - CODEOWNERS-Datei mit `docs/security/threat-model.md @security-team` für
    automatisches Reviewer-Routing.
  - Strukturierter Review-Bogen: STRIDE-Vollständigkeit, CAPEC-Bezug,
    DREAD/CVSS-Konsistenz, Mitigation-Realismus, Restrisiko-Akzeptanz.
  - Vier-Augen-Prinzip dokumentiert mit elektronischer Signatur (z. B.
    GPG-signierter Commit, signierter PDF-Export, Confluence-Approval-Workflow).
  Review-Ergebnis-Dokumentation:
  - Review-Block am Ende des Modells: Reviewer-Name, Rolle, Datum,
    Empfehlungen, Status (genehmigt / mit Auflagen / abgelehnt).
  - Bei Auflagen: Tickets im Issue-Tracker mit Verweis auf Threat-IDs.
  - Bei abgelehnter Risikoakzeptanz: Eskalation an verantwortliche Leitung mit
    schriftlicher Begründung.
- **EN:** At least one second person (e.g. security architect, senior
  engineer) has reviewed and approved the model.
  Suitable reviewer roles:
  - Security architect (CISSP, ISO 27001 Lead Implementer, iSAQB CPSA-A).
  - Senior engineer with domain knowledge and experience in STRIDE/CAPEC.
  - External reviewer for high-risk systems (penetration tester, BSI-certified
    auditor).
  - Data protection officer (DPO) for personal data (GDPR Art. 35 DPIA).
  Review methods and tools:
  - Pull request review in the Git repo: reviewer comments inline in
    `threat-model.md`, requests changes, or approves with Approve.
  - CODEOWNERS file with `docs/security/threat-model.md @security-team` for
    automatic reviewer routing.
  - Structured review checklist: STRIDE completeness, CAPEC reference,
    DREAD/CVSS consistency, mitigation realism, residual-risk acceptance.
  - Four-eyes principle documented with electronic signature (e.g. GPG-signed
    commit, signed PDF export, Confluence approval workflow).
  Review-result documentation:
  - Review block at the end of the model: reviewer name, role, date,
    recommendations, status (approved / approved with conditions / rejected).
  - For conditions: tickets in the issue tracker referencing threat IDs.
  - For rejected risk acceptance: escalation to management with written
    justification.
- **Akzeptanz / Acceptance:** Review-Vermerk mit Name, Rolle, Datum, Status,
  optional GPG-Signatur oder PDF-Approval; CODEOWNERS oder gleichwertige
  Routing-Regel im Repository.
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

#### CL-04-10: Ablage und Auffindbarkeit / Storage and Findability

- **DE:** Das Bedrohungsmodell liegt unter `docs/security/threat-model.md`
  oder in einem gleichwertig dokumentierten Pfad.
  Empfohlene Ablagestruktur im Projekt-Repo:
  - `docs/security/threat-model.md` – Hauptdokument im Markdown-Format.
  - `docs/security/dfd/` – Datenflussdiagramme als PlantUML, Mermaid,
    `.drawio` oder `.tm7` (Microsoft Threat Modeling Tool).
  - `docs/security/adr/` – Security ADRs, die aus dem Modell hervorgehen.
  - `docs/security/asvs-verification.md`, `supply-chain-evidence.md`,
    `zero-trust-applicability.md` als Quervernetzung.
  Verlinkung und Auffindbarkeit:
  - In `README.md` Abschnitt „Sicherheit" mit Link zum Bedrohungsmodell.
  - In `spec.md` Abschnitt „Sicherheit" und in `plan.md` Abschnitt „Risiken"
    konkrete Pfade nennen.
  - In `tasks.md` Verweis auf relevante Threat-IDs bei sicherheitskritischen
    Tasks.
  - Im internen Wiki (Confluence, Git Wiki, GitHub Wiki) Querverweis und
    Kurzfassung der Top-Risiken.
  - In `arc42 Section 8 Sicherheit` (`docs/architecture/08-concepts.md`)
    Verweis auf das Bedrohungsmodell.
  Alternative gleichwertige Pfade (mit Begründung dokumentiert):
  - In Compliance-Schwerpunkt-Repos: `compliance/threat-model.md`.
  - Bei mehreren Bounded Contexts: `services/<service>/docs/threat-model.md`
    pro Service mit übergreifender `docs/security/threat-model-overview.md`.
  Schutz vor Vergessen:
  - Linter-Regel im CI (`docs-linter.sh`), die das Vorhandensein von
    `docs/security/threat-model.md` prüft und bei Fehlen den Build fehlschlagen
    lässt.
- **EN:** The threat model lives under `docs/security/threat-model.md` or
  an equivalent documented path.
  Recommended storage structure in the project repo:
  - `docs/security/threat-model.md` – main document in Markdown.
  - `docs/security/dfd/` – data flow diagrams as PlantUML, Mermaid, `.drawio`,
    or `.tm7` (Microsoft Threat Modeling Tool).
  - `docs/security/adr/` – security ADRs derived from the model.
  - `docs/security/asvs-verification.md`, `supply-chain-evidence.md`,
    `zero-trust-applicability.md` for cross-linking.
  Linking and findability:
  - In `README.md` section "Security" with a link to the threat model.
  - In `spec.md` section "Security" and in `plan.md` section "Risks" name
    concrete paths.
  - In `tasks.md` reference relevant threat IDs for security-critical tasks.
  - In the internal wiki (Confluence, Git Wiki, GitHub Wiki) cross-reference
    and short version of the top risks.
  - In `arc42 Section 8 Security` (`docs/architecture/08-concepts.md`)
    reference to the threat model.
  Alternative equivalent paths (with documented justification):
  - In compliance-focused repos: `compliance/threat-model.md`.
  - For multiple bounded contexts: `services/<service>/docs/threat-model.md`
    per service with an overarching `docs/security/threat-model-overview.md`.
  Protection against omission:
  - Linter rule in CI (`docs-linter.sh`) that checks for the presence of
    `docs/security/threat-model.md` and fails the build if missing.
- **Akzeptanz / Acceptance:** Dateipfad in `README.md`, `spec.md`, `plan.md`,
  `tasks.md` und `arc42 Section 8` erwähnt; CI-Linter prüft Existenz; DFD
  versioniert und im Repo abgelegt.
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

**DE:** Erfüllt, wenn alle Punkte abgeschlossen sind, das Modell unterschrieben
ist und Risikoakzeptanzen schriftlich vorliegen.

**EN:** Fulfilled when every item is closed, the model is signed off, and
accepted risks are documented in writing.

### Glossar / Glossary

**DE:** Dieses Glossar erklärt die wichtigsten Begriffe dieser Checkliste in Alltagssprache. Es ändert keine Anforderungen, sondern macht die vorhandenen Prüfpunkte leichter verständlich.

**EN:** This glossary explains the most important terms in this checklist in plain language. It does not change requirements; it makes the existing review items easier to understand.

<a id="cl-04-glossar-threat-model"></a>

#### Bedrohungsmodell / Threat Model

- **DE:** Ein Bedrohungsmodell beschreibt, was geschützt werden muss, welche Angriffe möglich sind und welche Maßnahmen diese Risiken reduzieren.
- **EN:** A threat model describes what must be protected, which attacks are possible, and which measures reduce these risks.

<a id="cl-04-glossar-asset"></a>

#### Asset / Schutzwert

- **DE:** Ein Asset ist etwas Wertvolles, das geschützt werden muss, zum Beispiel Daten, ein Dienst, ein Schlüssel, ein System oder Verfügbarkeit.
- **EN:** An asset is something valuable that must be protected, for example data, a service, a key, a system, or availability.

<a id="cl-04-glossar-trust-boundary"></a>

#### Vertrauensgrenze / Trust Boundary

- **DE:** Eine Vertrauensgrenze trennt Bereiche mit unterschiedlichem Schutzbedarf oder unterschiedlicher Kontrolle. Daten, die diese Grenze überschreiten, brauchen besondere Prüfung.
- **EN:** A trust boundary separates areas with different protection needs or different control. Data crossing this boundary needs special review.

<a id="cl-04-glossar-data-flow"></a>

#### Datenfluss / Data Flow

- **DE:** Ein Datenfluss zeigt, welche Daten zwischen Komponenten, Personen oder Systemen übertragen werden. Er hilft, Risiken an Schnittstellen zu erkennen.
- **EN:** A data flow shows which data is transferred between components, people, or systems. It helps identify risks at interfaces.

<a id="cl-04-glossar-stride"></a>

#### STRIDE

- **DE:** STRIDE ist eine Methode zur Bedrohungsanalyse. Sie fragt nach Spoofing, Manipulation, Abstreiten, Informationsabfluss, Dienstverweigerung und Rechteausweitung.
- **EN:** STRIDE is a threat analysis method. It asks about spoofing, tampering, repudiation, information disclosure, denial of service, and elevation of privilege.

<a id="cl-04-glossar-capec"></a>

#### CAPEC

- **DE:** CAPEC ist ein Katalog bekannter Angriffsmuster. Er hilft beim Bedrohungsmodell, typische Angriffe systematisch zu erkennen.
- **EN:** CAPEC is a catalogue of known attack patterns. It helps threat models identify typical attacks systematically.

<a id="cl-04-glossar-risk-rating"></a>

#### Risikobewertung / Risk Rating

- **DE:** Eine Risikobewertung ordnet ein Risiko nach Wahrscheinlichkeit und Auswirkung ein. Daraus folgt, welche Maßnahmen zuerst umgesetzt werden müssen.
- **EN:** A risk rating classifies a risk by likelihood and impact. It shows which measures must be implemented first.

<a id="cl-04-glossar-mitigation"></a>

#### Maßnahme / Mitigation

- **DE:** Eine Maßnahme reduziert ein Risiko. Sie kann technisch, organisatorisch oder prozessbezogen sein und braucht meist einen Nachweis.
- **EN:** A mitigation reduces a risk. It can be technical, organisational, or process-related and usually needs evidence.

<a id="cl-04-glossar-evidenz"></a>

#### Evidenz / Evidence

- **DE:** Evidenz ist ein prüfbarer Nachweis. Das kann ein Link, Ticket, Scan-Bericht, Pull Request, Protokoll, Architekturdiagramm oder Dokument sein.
- **EN:** Evidence is verifiable proof. It can be a link, ticket, scan report, pull request, record, architecture diagram, or document.

<a id="cl-04-glossar-audit"></a>

#### Audit

- **DE:** Ein Audit ist eine formelle Prüfung. Dabei wird kontrolliert, ob Regeln eingehalten wurden und ob die Nachweise verständlich und auffindbar sind.
- **EN:** An audit is a formal review. It checks whether rules were followed and whether the evidence is understandable and findable.

### Versionshistorie / Version History

- **Version 1.0 (2026-04-27):** Erstfassung / Initial version
- **Version 1.1 (2026-04-27):** Erweiterte Durchführungshinweise, Quellen-URLs, Statusfelder und Beispiele / Extended guidance, source URLs, status fields, and examples
- **Version 1.2 (2026-06-15):** Aktualisierungspflicht um Cloud-/Provider-Wechsel, BSI C3A/C5, EU AI Act und DORA als Trigger ergänzt; synchron mit Richtlinie Sichere Entwicklung v2.9.0. / Extended update obligation with cloud/provider changes, BSI C3A/C5, EU AI Act, and DORA as triggers; synchronized with Richtlinie Sichere Entwicklung v2.9.0.

- **Version 1.3 (2026-06-16):** Verständlichkeit der Durchführungshinweise, Begründungs-, Evidenz- und Maßnahmenfelder für Entwickler:innen und Auszubildende präzisiert; CEFR-B2- und WCAG-2.2-AA-konforme Ausfüllhilfe ergänzt. / Refined understandability of implementation guidance, rationale, evidence, and action fields for developers and apprentices; added CEFR B2 and WCAG 2.2 AA conformant completion help.

- **Version 1.4 (2026-06-17):** Glossar und Begriff-Links für Entwickler:innen und Fachinformatik-Auszubildende ergänzt; wichtige Abkürzungen und Technologien in CEFR-B2-Sprache erklärt. / Added glossary and term links for developers and IT specialist apprentices; explained important abbreviations and technologies in CEFR B2 language.

---

- **Version 2.0.0 (2026-07-10):** Einheitliches zweiachsiges Statusmodell, stabile CL-IDs, Lernstufe, Rollen-, Evidenz-, Restrisiko- und Neubewertungsfelder sowie klare Trennung zwischen Vorlage und Projektnachweis eingeführt; synchron mit sichere-Entwicklung-Basis 3.0.0. / Added the unified two-axis status model, stable CL IDs, learning-stage, role, evidence, residual-risk, and re-evaluation fields, plus clear separation between template and project evidence; synchronized with secure-development baseline 3.0.0.
