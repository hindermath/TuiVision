<!--
Quelle / Source: generische Ausbildungs- und Pruefgrundlage, bereinigt am 2026-06-17.
Dieses Dokument ist organisationsneutral und als generische Ausbildungs- und Pruefgrundlage formuliert.
Source: generic training and review baseline, generalized on 2026-06-17.
This document is organization-neutral and written as a generic training and review baseline.
-->

> **DE:** Diese Checkliste ist generisch und projektunabhaengig. Sie ist als Ausbildungs-, Review- und Haertungsgrundlage gedacht. Eine Nichtanwendbarkeit muss als `N/A` mit kurzer Begruendung dokumentiert werden.
>
> **EN:** This checklist is generic and project-independent. It is intended as a training, review, and hardening baseline. Non-applicability must be documented as `N/A` with a short rationale.

## Checkliste 09 – KI-gestuetzte Codeerzeugung / AI-Assisted Code Generation

**Dokument-ID / Document ID:** CL-09
**Version / Version:** 2.0.0
**Baseline-Version / Baseline version:** 3.0.0
**Dokumenttyp / Document type:** Kanonische, wiederverwendbare Pruefvorlage / Canonical reusable review template

### Nachweisinstanz / Evidence Instance

Diese Datei ist eine wiederverwendbare Vorlage. Ausgefüllte Projektnachweise werden unter `docs/security/secure-development/<datum>-<scope>/` abgelegt und nennen Projekt, Scope, Prüfdatum, Baseline-Version, verantwortliche Person und Reviewer. / This file is a reusable template. Completed project evidence is stored under `docs/security/secure-development/<date>-<scope>/` and names project, scope, review date, baseline version, responsible person, and reviewer.

### Zweck / Purpose

**DE:** Diese Checkliste regelt den sicheren Einsatz von KI-Assistenten beim
Schreiben, Vervollständigen oder Refaktorieren von Code. KI-Werkzeuge können
Produktivität steigern, erzeugen aber nicht zuverlässig sicheren Code. Eine
explizite Prüfung ist daher Pflicht.

**EN:** This checklist governs the safe use of AI assistants for writing,
completing, or refactoring code. AI tools can boost productivity, but they
do not reliably produce secure code. Explicit review is therefore mandatory.

### Geltungsbereich / Scope

**DE:** Anzuwenden auf jeden Einsatz von KI-Code-Assistenten, lokalen LLMs
oder Agenten, deren Ausgabe in Produktivcode oder produktnahe Skripte fließt.

**EN:** Applies to every use of AI code assistants, local LLMs, or agents
whose output flows into production code or production-near scripts.

### Mitgeltende Dokumente / Related Documents

- Richtlinie Sichere Entwicklung
- CL_12_Agentische-KI-Sandbox
- Leitlinie für sichere Programmierung
- ISO/IEC 27002:2022 A.8.28
- OWASP Top 10 for Large Language Model Applications
- NIST AI Risk Management Framework (AI RMF 1.0)
- G7 „Software Bill of Materials for AI – Minimum Elements" (2026)

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

**DE:** Wenn agentische KI eine wesentliche Feature-Implementierung übernimmt, gilt zusätzlich `CL_12_Agentische-KI-Sandbox`. Das Feature soll über GitHub Spec Kit im Spec-Driven-Development-Ablauf (SDD) gesteuert werden. Dazu gehören Spezifikation, Klärung, Plan, Checkliste, Aufgaben, Analyse und Implementierung sowie die sechs Governance-Presets. Installation, Nachweis, Aktualität und inhaltliche Abdeckung der Governance-Presets werden in `CL_12_Agentische-KI-Sandbox` bewertet. Wenn die Spec-Kit-Artefakte die anwendbaren Prüfpunkte vollständig über eine Nachweis-Matrix abdecken, kann die separate manuelle Ausfüllung dieser Checkliste entfallen.

**EN:** If agentic AI performs a material feature implementation, `CL_12_Agentische-KI-Sandbox` also applies. The feature should be controlled through GitHub Spec Kit in the spec-driven development (SDD) flow. This includes specification, clarification, plan, checklist, tasks, analysis, implementation, and the six governance presets. Installation, evidence, currency, and content coverage of the governance presets are assessed in `CL_12_Agentische-KI-Sandbox`. If the Spec Kit artefacts fully cover the applicable review points through an evidence matrix, separate manual completion of this checklist may be omitted.

**DE:** Jeder Prüfpunkt muss deshalb drei Fragen beantworten: Was bedeutet die Anforderung im Projektalltag? Was ist konkret zu tun oder zu entscheiden? Welcher Nachweis zeigt das Ergebnis? Verwende Standard-IDs, Toolnamen und Abkürzungen nur zusammen mit einer kurzen Erklärung in Alltagssprache. Wenn ein Punkt für Auszubildende oder neue Teammitglieder nicht selbsterklärend ist, ergänze eine kurze Erklärung in der Begründung.

**EN:** Each item must therefore answer three questions: What does the requirement mean in daily project work? What exactly must be done or decided? Which evidence shows the result? Use standard IDs, tool names, and abbreviations only together with a short plain-language explanation. If an item is not self-explanatory for apprentices or new team members, add a short explanation in the rationale.

### Beispiel / Example

**DE:** Eine KI schlaegt eine neue Bibliothek vor. Vor dem Merge prueft das Team, ob das Paket wirklich existiert, gepflegt ist, keine kritischen CVEs hat und lizenzrechtlich erlaubt ist. Der PR markiert den KI-Anteil.

**EN:** An AI suggests a new library. Before merge, the team checks that the package really exists, is maintained, has no critical CVEs, and is allowed by licence. The PR marks the AI contribution.

### A11Y-Hinweise / A11Y Notes

**DE:** Beim Ausfüllen dieser Checkliste müssen alle Nachweise auch textlich verständlich sein. Verweise sollen beschreibende Linktexte haben. Screenshots, Diagramme oder Scan-Auszüge brauchen eine kurze Textbeschreibung. Der Status darf nicht nur über Farbe erkennbar sein.

**EN:** When this checklist is filled in, all evidence must also be understandable as text. References should use descriptive link text. Screenshots, diagrams, or scan extracts need a short text description. The status must not be shown by color alone.

### Wichtige Begriffe / Key Terms

**DE:** Die folgenden Begriffe kommen in dieser Checkliste vor. Die Links springen zum Glossar dieses Kapitels, damit Auszubildende und Entwickler:innen ohne Sicherheits-Spezialwissen die Begriffe direkt nachlesen können.

**EN:** The following terms appear in this checklist. The links jump to this chapter's glossary so that apprentices and developers without specialist security knowledge can look them up directly.

- [KI-Code-Assistenz / AI Coding Assistance](#cl-09-glossar-ai-code-assistenz)
- [LLM / Large Language Model](#cl-09-glossar-llm)
- [Prompt](#cl-09-glossar-prompt)
- [Menschliche Prüfung / Human Review](#cl-09-glossar-human-review)
- [Vier-Augen-Prinzip / Four-Eyes Rule](#cl-09-glossar-four-eyes)
- [CVE](#cl-09-glossar-cve)
- [Halluziniertes Paket / Hallucinated Package](#cl-09-glossar-hallucinated-package)
- [Abhängigkeit / Dependency](#cl-09-glossar-dependency)
- [Personenbezogene Daten / Personal Data](#cl-09-glossar-personal-data)
- [Telemetrie / Telemetry](#cl-09-glossar-telemetry)
- [Audit-Spur / Audit Trail](#cl-09-glossar-audit-trail)
- [AI-SBOM / ML-BOM](#cl-09-glossar-ai-sbom-ml-bom)
- [EU AI Act](#cl-09-glossar-eu-ai-act)
- [Spec Kit](#cl-09-glossar-spec-kit)
- [SDD / Specification-Driven Development](#cl-09-glossar-sdd)
- [Preset](#cl-09-glossar-preset)
- [Nachweis-Matrix / Evidence Matrix](#cl-09-glossar-nachweis-matrix)

### Checkliste / Checklist

#### CL-09-01: Genehmigte KI-Werkzeuge / Approved AI Tools

- **DE:** Es kommen nur Werkzeuge zum Einsatz, die in der Liste der
  genehmigten Software stehen. Cloud-basierte und lokale Werkzeuge sind
  getrennt freigegeben, weil sie unterschiedliche Datenschutz-, Lizenz-
  und Auftragsverarbeitungs-Profile haben. Genehmigte Cloud-Werkzeuge
  (Beispielkatalog mit Auftragsverarbeitung gemäß DSGVO Art. 28):
  GitHub Copilot Business und Enterprise (kein Training auf Kundendaten,
  Indemnification, EU-Datenresidenz Enterprise), Microsoft 365 Copilot
  (DPA, EU Data Boundary), Anthropic Claude (Enterprise-Plan mit Zero
  Data Retention nach Verhandlung), OpenAI ChatGPT Enterprise/Team mit
  ZDR, Google Gemini Code Assist (Enterprise-DPA), JetBrains AI Assistant
  (Enterprise-Plan, Datenresidenz EU), AWS Bedrock/Q Developer in eigener
  AWS-Region, Azure OpenAI Service in EU-Region. Genehmigte lokale
  Werkzeuge ohne Cloud-Datenfluss: Continue.dev mit lokalem Ollama,
  Tabby ML mit eigener GPU, Codeium Self-Hosted, llama.cpp mit Code Llama
  70B/Qwen2.5-Coder/DeepSeek-Coder-V2/StarCoder2, LM Studio, vLLM, GPT4All.
  Verboten ohne explizite Freigabe: kostenlose Web-Chat-Bots ohne DPA,
  Browser-Plugins unbekannter Herkunft, Forks aus inoffiziellen Stores.
  Software-Inventar (CMDB Snipe-IT, ServiceNow CMDB, Atlassian Insight)
  pflegt pro Werkzeug: Hersteller, Lizenztyp (Pro/Business/Enterprise),
  EU/US-Datenresidenz, ZDR-Status (Zero Data Retention), Trainings-Opt-out,
  DPA-Datum, Vendor-Risk-Score. SBOM-Eintrag (CycloneDX) für jede lokale
  Modell-Datei mit SHA-256-Hash. Beispiel für `docs/security/ai-tools-inventory.md`:
  Tabelle mit Spalten: Werkzeug, Variante, Vendor, EU-DPA-Datum, ZDR,
  `excludeFiles`-Profil, freigegeben-bis-Datum, Owner Security Champion.
  Vierteljährliche Re-Evaluation, neue Werkzeuge nur nach
  Vendor-Security-Assessment (CAIQ, SOC 2 Type II, ISO/IEC 27001-Zertifikat,
  ISO/IEC 42001 AI-Management-System falls verfügbar). Referenzen:
  ISO/IEC 27001 A.5.23 (Cloud-Services), DSGVO Art. 28, NIST AI RMF
  GOVERN-1.1, OWASP Top 10 for LLM Applications LLM05:2025 (Improper Output Handling), EU AI
  Act Verordnung (EU) 2024/1689.
- **EN:** Only tools listed in the approved software inventory are used.
  Cloud-based and local tools are approved separately because they have
  different data protection, licensing, and data processing profiles.
  Approved cloud tools (example catalogue with data processing under GDPR
  Art. 28): GitHub Copilot Business and Enterprise (no training on
  customer data, indemnification, EU data residency in Enterprise),
  Microsoft 365 Copilot (DPA, EU Data Boundary), Anthropic Claude
  (Enterprise plan with Zero Data Retention after negotiation), OpenAI
  ChatGPT Enterprise/Team with ZDR, Google Gemini Code Assist (Enterprise
  DPA), JetBrains AI Assistant (Enterprise plan, EU data residency), AWS
  Bedrock/Q Developer in own AWS region, Azure OpenAI Service in EU
  region. Approved local tools without cloud data flow: Continue.dev with
  local Ollama, Tabby ML with own GPU, Codeium Self-Hosted, llama.cpp
  with Code Llama 70B/Qwen2.5-Coder/DeepSeek-Coder-V2/StarCoder2, LM
  Studio, vLLM, GPT4All. Forbidden without explicit approval: free web
  chatbots without DPA, browser plugins of unknown origin, forks from
  unofficial stores. Software inventory (CMDB Snipe-IT, ServiceNow CMDB,
  Atlassian Insight) maintains per tool: vendor, licence type
  (Pro/Business/Enterprise), EU/US data residency, ZDR (Zero Data
  Retention) status, training opt-out, DPA date, vendor risk score. SBOM
  entry (CycloneDX) for each local model file with SHA-256 hash. Example
  for `docs/security/ai-tools-inventory.md`: table with columns: tool,
  variant, vendor, EU DPA date, ZDR, `excludeFiles` profile, approved-
  until date, security champion owner. Quarterly re-evaluation, new tools
  only after vendor security assessment (CAIQ, SOC 2 Type II, ISO/IEC
  27001 certificate, ISO/IEC 42001 AI management system if available).
  References: ISO/IEC 27001 A.5.23 (cloud services), GDPR Art. 28, NIST
  AI RMF GOVERN-1.1, OWASP Top 10 for LLM Applications LLM05:2025 (Improper Output Handling),
  EU AI Act Regulation (EU) 2024/1689.
- **Akzeptanz / Acceptance:** `docs/security/ai-tools-inventory.md`
  vorhanden mit Vendor, Lizenztyp, EU-DPA-Datum, ZDR-Status, freigegeben-bis-Datum,
  Owner; pro Werkzeug Datenschutzhinweis und SOC 2/ISO 27001-Nachweis;
  vierteljährliche Re-Evaluation dokumentiert.
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

#### CL-09-02: Pflicht zur menschlichen Überprüfung / Mandatory Human Review

- **DE:** Jede KI-Ausgabe wird vor dem Commit von einer Person gelesen
  und inhaltlich geprüft. Ein Blind-Commit ist nicht zulässig. Reviewer-
  Pflichten: jeder vorgeschlagene Codeblock zeilenweise lesen, gegen
  Anforderung in Issue/Ticket abgleichen, auf Halluzinationen prüfen
  (existiert die API-Funktion wirklich? — gegen offizielle Doku
  verifizieren, MDN, JavaDoc, Microsoft Learn, devdocs.io), Sicherheits-
  Implikationen bewerten (Eingabe-Validierung, Output-Encoding, Auth/Authz,
  Krypto), Tests selber lesen (KI-Tests können tautologisch sein:
  `assertEqual(getValue(), getValue())`). Blind-Commit-Verbot gilt auch
  für vermeintlich triviale Vervollständigungen — IDE-Autocompletion mit
  Tab-Bestätigung ist eine bewusste Annahme, kein passiver Akt. Commit-
  Trailer für KI-Beteiligung: `Co-developed-with: Claude` oder `Assisted-by:
  GitHub Copilot` als nachvollziehbarer Hinweis (wie `Co-Authored-By` für
  Pair Programming). Pull-Request-Beschreibung enthält Abschnitt „AI
  Assistance" mit Werkzeug, Umfang (z. B. „Boilerplate Test Setup",
  „Refactoring", „Implementierung kompletter Funktion X"), Reviewer-
  Vermerk „Manuell zeilenweise geprüft am YYYY-MM-DD von <Name>".
  Code-Owner-Review (`CODEOWNERS`-Datei) erzwingt Pflicht-Reviewer für
  Sicherheitsbereiche. PR-Template (`.github/pull_request_template.md`)
  enthält Checkbox „Ich habe alle KI-Vorschläge zeilenweise gelesen und
  verstanden". Tooling: GitHub Copilot Code Review (Vorschläge, nicht
  Ersatz), CodeRabbit, Sourcery — diese ergänzen menschliche Reviews,
  ersetzen sie nicht. Anti-Patterns: „LGTM" ohne Detail, Approval mit
  >500 Zeilen Diff in <2 min, Auto-Merge unmittelbar nach KI-Generierung.
  Referenzen: NIST AI RMF MEASURE-2.7 (Human Oversight), OWASP Top 10 for
  LLM Applications `LLM09:2025` (Misinformation), ISO/IEC 27001 A.8.4
  (Access to source code),
  CRA Anhang I Teil II 2.4 (sichere Standardkonfigurationen,
  Code-Review).
- **EN:** Every AI output is read and reviewed by a person before commit.
  Blind commits are not allowed. Reviewer duties: read each suggested
  code block line by line, compare against the requirement in the
  issue/ticket, check for hallucinations (does the API function really
  exist? — verify against official docs, MDN, JavaDoc, Microsoft Learn,
  devdocs.io), evaluate security implications (input validation, output
  encoding, auth/authz, crypto), read the tests yourself (AI tests can
  be tautological: `assertEqual(getValue(), getValue())`). The
  blind-commit ban also applies to seemingly trivial completions — IDE
  autocompletion with tab confirmation is a deliberate acceptance, not a
  passive act. Commit trailer for AI involvement: `Co-developed-with:
  Claude` or `Assisted-by: GitHub Copilot` as a traceable note (like
  `Co-Authored-By` for pair programming). Pull request description
  contains an "AI Assistance" section with tool, scope (e.g., "boilerplate
  test setup", "refactoring", "full implementation of function X"),
  reviewer note "Manually reviewed line by line on YYYY-MM-DD by <name>".
  Code owner review (`CODEOWNERS` file) enforces mandatory reviewers for
  security areas. PR template (`.github/pull_request_template.md`)
  contains a checkbox "I have read and understood all AI suggestions
  line by line". Tooling: GitHub Copilot Code Review (suggestions, not
  replacement), CodeRabbit, Sourcery — these complement human reviews,
  they do not replace them. Anti-patterns: "LGTM" without detail,
  approval with >500 lines diff in <2 min, auto-merge immediately after
  AI generation. References: NIST AI RMF MEASURE-2.7 (Human Oversight),
  OWASP Top 10 for LLM Applications LLM09:2025 (Misinformation), ISO/IEC 27001 A.8.4 (Access to
  source code), CRA Annex I Part II 2.4 (secure default configurations,
  code review).
- **Akzeptanz / Acceptance:** Pull-Request-Vermerk oder Commit-Trailer
  mit Reviewer-Hinweis vorhanden; PR-Template-Checkbox „zeilenweise
  geprüft" abgehakt; CODEOWNERS-Pflicht-Review für Sicherheitsbereiche
  erzwungen; keine Auto-Merges direkt nach KI-Generierung.
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

#### CL-09-03: Vier-Augen-Prinzip bei kritischer Logik / Four-Eyes Rule for Critical Logic

- **DE:** Sicherheitskritische Logik (Authentifizierung, Autorisierung,
  Eingabevalidierung, Krypto, Sitzungsverwaltung, Zahlungspfade,
  PII-Verarbeitung, Audit-Logging, Datei-Upload-Pfade, Datenbank-
  Migrationen, Berechtigungs-Änderungen, Lieferketten-Konfiguration wie
  CI/CD-YAML, Dockerfile, terraform .tf) wird zusätzlich von einer
  zweiten Person geprüft. Vier-Augen erzwingen via Branch Protection
  `required_approving_review_count: 2` für `main`/`release/*`-Branches
  oder via `CODEOWNERS` mit Pflicht-Reviewern aus Security-Team:
  ```text
  /src/auth/                  @security-team @platform-team
  /src/crypto/                @security-team @senior-architect
  /src/payment/               @security-team @finance-engineering
  /infrastructure/terraform/  @platform-team @security-team
  /.github/workflows/         @security-team @devops-team
  /Dockerfile                 @security-team @devops-team
  /db/migrations/             @senior-backend @security-team
  /docs/security/             @security-team
  ```
  Zweiter Reviewer ist nicht der Pull-Request-Autor (GitHub `dismiss_stale_reviews:
  true` und `require_review_from_code_owners: true`). Bei KI-generierter
  sicherheitskritischer Logik gilt zusätzlich: Senior-Engineer mit
  domänenspezifischer Erfahrung als zweiter Reviewer (kein Junior + Junior).
  Threat-Modeling-Ergänzung im PR: Welche STRIDE-Kategorie ist betroffen
  (Spoofing, Tampering, Repudiation, Information Disclosure, Denial of
  Service, Elevation of Privilege)? Statisch-Analyse-Ergebnisse (Semgrep
  mit `p/security-audit`, CodeQL mit `security-extended` Suite, SonarQube
  mit Security Hotspots, Snyk Code) müssen vor Approval gezeigt werden.
  Beispielhafter PR-Workflow: Autor pusht KI-Vorschlag → CI läuft (SAST,
  DAST, Tests) → 1. Reviewer (Senior Domain) prüft fachlich → 2. Reviewer
  (Security Champion oder Security-Team) prüft Threat-Surface → Merge
  nur, wenn beide approven und alle CI-Gates grün. Referenzen: ISO/IEC
  27001 A.8.30 (Outsourced Development — auch für KI-„Mitautoren"
  anwendbar), NIST SSDF SP 800-218 PW.7.1 (peer review), OWASP ASVS V1.1.4
  (multiple-eyes review for security-critical logic), CRA Anhang I Teil
  II 2.5 (Sicherheitstests vor Inbetriebnahme).
- **EN:** Security-critical logic (auth, input validation, crypto,
  session management, payment paths, PII processing, audit logging, file
  upload paths, database migrations, permission changes, supply chain
  configuration such as CI/CD YAML, Dockerfile, terraform .tf) is
  reviewed by a second person on top. Enforce four-eyes via branch
  protection `required_approving_review_count: 2` for `main`/`release/*`
  branches or via `CODEOWNERS` with mandatory reviewers from the
  security team:
  ```text
  /src/auth/                  @security-team @platform-team
  /src/crypto/                @security-team @senior-architect
  /src/payment/               @security-team @finance-engineering
  /infrastructure/terraform/  @platform-team @security-team
  /.github/workflows/         @security-team @devops-team
  /Dockerfile                 @security-team @devops-team
  /db/migrations/             @senior-backend @security-team
  /docs/security/             @security-team
  ```
  The second reviewer is not the PR author (GitHub `dismiss_stale_reviews:
  true` and `require_review_from_code_owners: true`). For AI-generated
  security-critical logic an additional rule applies: senior engineer
  with domain experience as second reviewer (no Junior + Junior).
  Threat modeling addition in the PR: which STRIDE category is affected
  (Spoofing, Tampering, Repudiation, Information Disclosure, Denial of
  Service, Elevation of Privilege)? Static analysis results (Semgrep
  with `p/security-audit`, CodeQL with `security-extended` suite,
  SonarQube with security hotspots, Snyk Code) must be shown before
  approval. Example PR workflow: author pushes AI suggestion → CI runs
  (SAST, DAST, tests) → 1st reviewer (senior domain) reviews
  functionally → 2nd reviewer (security champion or security team)
  reviews threat surface → merge only if both approve and all CI gates
  are green. References: ISO/IEC 27001 A.8.30 (outsourced development —
  applicable to AI "co-authors" as well), NIST SSDF SP 800-218 PW.7.1
  (peer review), OWASP ASVS V1.1.4 (multiple-eyes review for security-
  critical logic), CRA Annex I Part II 2.5 (security tests before
  release).
- **Akzeptanz / Acceptance:** Zweite Review-Signatur im Pull Request
  durch Senior/Security-Team via CODEOWNERS erzwungen; Branch Protection
  `required_approving_review_count: 2` für main/release/*; STRIDE-Bewertung
  und SAST-Ergebnis im PR dokumentiert.
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

#### CL-09-04: CVE-Prüfung für vorgeschlagene Abhängigkeiten / CVE Check for Suggested Dependencies

- **DE:** Wenn die KI eine neue Abhängigkeit vorschlägt, wird die
  Bibliothek auf Existenz, Pflegezustand und bekannte CVEs geprüft,
  bevor sie aufgenommen wird. Konkrete Prüfschritte: (1) Existenz auf
  offizieller Registry verifizieren — `npm view <package>`, `pip show
  <package>` und `pypi.org/project/<name>`, `cargo info <crate>` und
  `crates.io`, `go list -m <module>`, Maven Central
  `search.maven.org/artifact/...`, NuGet `nuget.org/packages/...`. (2)
  Pflegezustand: letzter Commit ≤ 6 Monate, ≥ 1 Maintainer (kein
  Single-Maintainer-Risiko bei kritischen Abhängigkeiten),
  Repository-Aktivität, GitHub Stars als grobe Heuristik (nicht
  ausschließlich), Issues-Antwortzeit. (3) CVE-Prüfung über NVD
  `nvd.nist.gov/vuln/search`, GitHub Advisory Database
  `github.com/advisories`, OSV-Datenbank `osv.dev`, Snyk Vulnerability
  DB, EPSS-Score (Exploit Prediction Scoring System) bei `epss.cyentia.com`
  — Score > 0.7 bedeutet hohe Ausnutzungswahrscheinlichkeit. (4)
  OpenSSF Scorecard `securityscorecards.dev` — Score < 5 ist
  Warnsignal: prüft Maintained, CI-Tests, Code-Review, Branch-Protection,
  signierte Releases, Vulnerabilities, SAST. (5) deps.dev (Google) für
  transitive Abhängigkeiten und Lizenzen. (6) Mindestalter (z. B. via
  npm `minimumReleaseAge: "3 days"`) gegen Typosquatting/Malware-Pakete
  in frischen Releases. Tooling-Pipeline-Integration: `npm audit
  --audit-level=high`, `pip-audit`, `cargo audit`, `govulncheck`, OSV-
  Scanner, Trivy, Grype, Dependabot/Renovate Auto-PRs, Snyk Open Source.
  Risiko-Klassen für Reviewer-Entscheidung: blockierend bei kritischer
  CVE ohne Patch (CVSS ≥ 9.0), bei Single-Maintainer mit verlassener
  Maintenance, bei OpenSSF Scorecard < 3, bei abweichendem
  Maintainer-Profil zwischen Vorschlag und Realität (mögliches
  Halluzinations-Indiz). PR-Vermerk: „Dependency Audit: <package>
  v<version>, Last commit YYYY-MM-DD, OpenSSF Score X/10, CVEs: keine,
  EPSS-Max: 0.X, Lizenz: MIT, Mindestalter: 14 Tage, Reviewer: <Name>".
  Referenzen: NIST SP 800-161r1 (C-SCRM Cybersecurity Supply Chain Risk
  Management), OWASP Top 10 A06:2021 (Vulnerable and Outdated
  Components), CRA Anhang I Teil II 2.2 (Schwachstellenmanagement).
- **EN:** When the AI suggests a new dependency, the library is checked
  for existence, maintenance status, and known CVEs before adoption.
  Concrete check steps: (1) Verify existence on the official registry —
  `npm view <package>`, `pip show <package>` and
  `pypi.org/project/<name>`, `cargo info <crate>` and `crates.io`, `go
  list -m <module>`, Maven Central `search.maven.org/artifact/...`,
  NuGet `nuget.org/packages/...`. (2) Maintenance status: last commit ≤
  6 months, ≥ 1 maintainer (no single-maintainer risk for critical
  dependencies), repository activity, GitHub stars as a rough heuristic
  (not exclusively), issue response time. (3) CVE check via NVD
  `nvd.nist.gov/vuln/search`, GitHub Advisory Database
  `github.com/advisories`, OSV database `osv.dev`, Snyk Vulnerability
  DB, EPSS score (Exploit Prediction Scoring System) at
  `epss.cyentia.com` — score > 0.7 means high exploitation likelihood.
  (4) OpenSSF Scorecard `securityscorecards.dev` — score < 5 is a
  warning signal: checks maintained, CI tests, code review, branch
  protection, signed releases, vulnerabilities, SAST. (5) deps.dev
  (Google) for transitive dependencies and licences. (6) Minimum age
  (e.g., via npm `minimumReleaseAge: "3 days"`) against typosquatting
  and malware packages in fresh releases. Tooling pipeline integration:
  `npm audit --audit-level=high`, `pip-audit`, `cargo audit`,
  `govulncheck`, OSV-Scanner, Trivy, Grype, Dependabot/Renovate auto
  PRs, Snyk Open Source. Risk classes for reviewer decision: blocking
  on critical CVE without patch (CVSS ≥ 9.0), single maintainer with
  abandoned maintenance, OpenSSF Scorecard < 3, deviating maintainer
  profile between suggestion and reality (possible hallucination
  indicator). PR note: "Dependency Audit: <package> v<version>, Last
  commit YYYY-MM-DD, OpenSSF Score X/10, CVEs: none, EPSS-Max: 0.X,
  Licence: MIT, Minimum age: 14 days, Reviewer: <name>". References:
  NIST SP 800-161r1 (C-SCRM Cybersecurity Supply Chain Risk
  Management), OWASP Top 10 A06:2021 (Vulnerable and Outdated
  Components), CRA Annex I Part II 2.2 (vulnerability management).
- **Akzeptanz / Acceptance:** Prüfvermerk im PR oder im Abhängigkeits-Audit
  mit CVE-Status (NVD/OSV/Snyk), OpenSSF Scorecard, EPSS, Lizenz,
  Mindestalter, Maintainer-Profil; CI-Gate via `npm audit`/`pip-audit`/
  `osv-scanner`/Trivy.
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

#### CL-09-05: Schutz vor halluzinierten Paketen / Protection Against Hallucinated Packages

- **DE:** KI-Modelle erfinden manchmal Bibliotheken
  („Slopsquatting"-Risiko). Vorgeschlagene Paketnamen werden gegen die
  offizielle Registry geprüft, weil Angreifer halluzinierte Paketnamen
  als typosquatted Schadpakete registrieren können (Beispiel-Vorfälle:
  `huggingface-cli` Halluzinationen führten 2024 zu Schadpaket-Anmeldungen
  auf PyPI; gleicher Angriffsvektor auf npm mit `node-fetch-2`,
  `aws-sdk-s3-helper` etc.). Prüfschritte: (1) Existenz auf offizieller
  Registry verifizieren (siehe Item 4); (2) Veröffentlichungsdatum
  prüfen — Pakete < 30 Tage alt mit verdächtigem Namen sind starkes
  Red-Flag, automatische Sperrung via npm `minimumReleaseAge: "30
  days"` für Lock-Dateien oder Renovate `minimumReleaseAge`-Regel; (3)
  Maintainer-Profil prüfen — neue Account ohne andere Pakete oder
  Verbindung zur erwarteten Organisation ist verdächtig (`npm owner ls
  <package>`, `pip show <package> | grep Author`, GitHub-Profil-Alter
  und -Aktivität); (4) Download-Statistik prüfen — < 100 Downloads/Woche
  bei vermeintlich populärem Paketnamen ist Halluzinations-Indiz; (5)
  Cross-Check Levenshtein-Distanz zu populären Paketen
  (`packagename-typosquat-detector`, npm `package-name-similarity`,
  `pip-audit --vulnerability-service all`); (6) GitHub-Repository-Link
  und README aufrufen — fehlende oder generisch wirkende Inhalte sind
  Red Flags. Bekannte Halluzinations-Muster: Bindestrich-Variationen
  (`requests-http` statt `requests`), Singular/Plural-Wechsel (`eslint-plugin-react`
  vs. `eslint-plugins-react`), Versions-Suffixe (`lodash3`, `react-dom2`),
  scope-Verschiebungen (`@aws-sdk/s3` vs. `@aws-sdks/s3`). Automatisierte
  Verteidigung: `socket.dev` (Supply-chain-Sicherheit, prüft neue
  Pakete auf Anomalien wie obfuscated code, install scripts,
  Telemetrie-Code, typosquatting), `phylum.io`,
  `apiiro/dependency-combobulator`, GitHub Native (npm provenance,
  PyPI Trusted Publishers via OIDC). CI-Gate: `npm install` mit
  `--ignore-scripts` standardmäßig, danach manuelle Allowlist für
  bekannte sichere `postinstall`-Skripte. Referenzen: OWASP LLM Top 10
  LLM05 (Improper Output Handling), Lasso Security (Slopsquatting-
  Forschung 2024), JFrog SecurityResearch (PyPI-Halluzinations-Studie),
  CRA Anhang I Teil II 2.2 (Schwachstellenmanagement Lieferkette).
- **EN:** AI models sometimes invent libraries ("slopsquatting" risk).
  Suggested package names are verified against the official registry,
  because attackers can register hallucinated package names as
  typosquatted malicious packages (example incidents: `huggingface-cli`
  hallucinations led to malicious package uploads on PyPI in 2024;
  same attack vector on npm with `node-fetch-2`, `aws-sdk-s3-helper`,
  etc.). Check steps: (1) Verify existence on the official registry
  (see item 4); (2) Check publication date — packages < 30 days old
  with suspicious names are a strong red flag, auto-block via npm
  `minimumReleaseAge: "30 days"` for lock files or Renovate
  `minimumReleaseAge` rule; (3) Check maintainer profile — new account
  without other packages or connection to the expected organization is
  suspicious (`npm owner ls <package>`, `pip show <package> | grep
  Author`, GitHub profile age and activity); (4) Check download
  statistics — < 100 downloads/week for an allegedly popular package
  name is a hallucination indicator; (5) Cross-check Levenshtein
  distance to popular packages
  (`packagename-typosquat-detector`, npm `package-name-similarity`,
  `pip-audit --vulnerability-service all`); (6) Open the GitHub
  repository link and README — missing or generic content is a red
  flag. Known hallucination patterns: hyphen variations
  (`requests-http` instead of `requests`), singular/plural swaps
  (`eslint-plugin-react` vs. `eslint-plugins-react`), version suffixes
  (`lodash3`, `react-dom2`), scope shifts (`@aws-sdk/s3` vs.
  `@aws-sdks/s3`). Automated defence: `socket.dev` (supply-chain
  security, checks new packages for anomalies like obfuscated code,
  install scripts, telemetry code, typosquatting), `phylum.io`,
  `apiiro/dependency-combobulator`, GitHub Native (npm provenance, PyPI
  Trusted Publishers via OIDC). CI gate: `npm install` with
  `--ignore-scripts` by default, then a manual allowlist for known-safe
  `postinstall` scripts. References: OWASP LLM Top 10 LLM05 (Improper
  Output Handling), Lasso Security (slopsquatting research 2024),
  JFrog Security Research (PyPI hallucination study), CRA Annex I Part
  II 2.2 (supply chain vulnerability management).
- **Akzeptanz / Acceptance:** Registry-Prüfung dokumentiert mit
  Veröffentlichungsdatum, Maintainer-Profil, Download-Statistik,
  Levenshtein-Cross-Check zu populären Paketen; CI-Gate `socket.dev`/
  `phylum.io` aktiv; `--ignore-scripts` als Default mit Allowlist.
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

#### CL-09-06: Lizenz-Klärung / Licence Clearance

- **DE:** KI-erzeugte Codeschnipsel können Lizenzfragen aufwerfen,
  weil das Modell auf Trainingsdaten mit unterschiedlichen Lizenzen
  (MIT, Apache-2.0, BSD, GPLv2, GPLv3, AGPL, proprietär ohne Lizenz)
  trainiert wurde und längere wörtliche Zitate aus GPL-Code rechtlich
  problematisch sein können (vgl. Doe v. GitHub Sammelklage 2022,
  weiterhin offen). Zweifelhafte Schnipsel werden umgeschrieben oder
  durch einen klar lizenzierten Block ersetzt. Konkrete Maßnahmen: (1)
  Tools mit Code-Reference-Filter aktivieren — GitHub Copilot
  „Suggestions matching public code: Block" (Settings → Copilot → Block
  matching public code), AWS CodeWhisperer/Q Developer „Reference
  Tracker", Tabnine „Restrict suggestions matching public code"; (2)
  Bei längeren Code-Vorschlägen (≥ 5 Zeilen, nicht-trivial)
  Plagiats-Suche durchführen — `grep` durch GitHub-Suche
  (`https://github.com/search?type=code&q="<10-15 Token Schnipsel>"`),
  CodeProbe, BlueOak, FOSSology, ScanCode Toolkit, Black Duck Code Sight,
  Synopsys Code Insight; (3) Bei Treffer mit GPL/AGPL/CC-BY-SA: Block
  umschreiben oder Lizenz beachten (eigenes Projekt muss kompatible
  Lizenz haben; AGPL ist für SaaS besonders restriktiv); (4)
  Lizenz-Verträglichkeit für gesamtes Projekt prüfen — Lizenz-Matrix in
  `LICENSE-COMPATIBILITY.md` mit erlaubten Lizenzen (MIT, BSD-2/3-Clause,
  Apache-2.0, ISC, MPL-2.0), bedingt erlaubt (LGPL bei dynamischer
  Verlinkung), verboten (GPL/AGPL für proprietäre Produkte, WTFPL
  unprofessionell, „Public Domain" rechtlich unklar in DE/EU); (5)
  Vendor-Indemnification nutzen — GitHub Copilot Business/Enterprise
  übernimmt Rechtsbeistand bei Lizenz-Streit, AWS Q Developer Pro mit
  IP-Indemnification, Anthropic Claude Enterprise mit IP-Indemnity nach
  Verhandlung. (6) Author-Trailer im Commit: `Co-developed-with: <Tool>
  v<Version>` als nachvollziehbarer Hinweis. SBOM-Eintrag (CycloneDX
  oder SPDX) für jede neue Abhängigkeit mit `licenseId` und SPDX-
  Lizenz-Identifikator. Tooling: SCANCODE-TOOLKIT, REUSE-Tool für
  SPDX-konforme Lizenzkennzeichnung in Quelldateien (SPDX-License-Identifier-
  Header), `pip-licenses`, `license-checker` (npm), `cargo-deny`,
  `go-licenses`. Referenzen: SPDX `spdx.org/licenses/`, OpenChain ISO/IEC
  5230 (Open Source License Compliance Management), REUSE Software
  `reuse.software/`, Linux Foundation TODO Group, ISO/IEC 27001 A.5.32
  (intellectual property rights), CRA Anhang I Teil II 1.f (sichere
  Standardkonfiguration und keine bekannten Schwachstellen).
- **EN:** AI-generated code snippets can raise licence questions,
  because the model was trained on data with different licences (MIT,
  Apache-2.0, BSD, GPLv2, GPLv3, AGPL, proprietary without licence) and
  longer verbatim quotes from GPL code can be legally problematic (cf.
  Doe v. GitHub class action 2022, still open). Doubtful snippets are
  rewritten or replaced by a clearly licensed block. Concrete measures:
  (1) Enable code reference filters in tools — GitHub Copilot
  "Suggestions matching public code: Block" (Settings → Copilot → Block
  matching public code), AWS CodeWhisperer/Q Developer "Reference
  Tracker", Tabnine "Restrict suggestions matching public code"; (2)
  For longer code suggestions (≥ 5 lines, non-trivial) perform
  plagiarism search — `grep` through GitHub search
  (`https://github.com/search?type=code&q="<10-15 token snippet>"`),
  CodeProbe, BlueOak, FOSSology, ScanCode Toolkit, Black Duck Code
  Sight, Synopsys Code Insight; (3) On hit with GPL/AGPL/CC-BY-SA:
  rewrite the block or honour the licence (your project needs a
  compatible licence; AGPL is particularly restrictive for SaaS); (4)
  Check licence compatibility for the entire project — licence matrix
  in `LICENSE-COMPATIBILITY.md` with allowed licences (MIT, BSD-2/3-
  Clause, Apache-2.0, ISC, MPL-2.0), conditionally allowed (LGPL for
  dynamic linking), forbidden (GPL/AGPL for proprietary products, WTFPL
  unprofessional, "Public Domain" legally unclear in DE/EU); (5) Use
  vendor indemnification — GitHub Copilot Business/Enterprise covers
  legal defence in licence disputes, AWS Q Developer Pro with IP
  indemnification, Anthropic Claude Enterprise with IP indemnity after
  negotiation. (6) Author trailer in commit: `Co-developed-with:
  <tool> v<version>` as a traceable note. SBOM entry (CycloneDX or
  SPDX) for each new dependency with `licenseId` and SPDX licence
  identifier. Tooling: SCANCODE-TOOLKIT, REUSE Tool for SPDX-compliant
  licence labelling in source files (SPDX-License-Identifier headers),
  `pip-licenses`, `license-checker` (npm), `cargo-deny`,
  `go-licenses`. References: SPDX `spdx.org/licenses/`, OpenChain ISO/IEC
  5230 (Open Source License Compliance Management), REUSE Software
  `reuse.software/`, Linux Foundation TODO Group, ISO/IEC 27001 A.5.32
  (intellectual property rights), CRA Annex I Part II 1.f (secure
  default configuration and no known vulnerabilities).
- **Akzeptanz / Acceptance:** Hinweis im PR, wenn KI-Vorschlag
  übernommen wurde, mit Lizenz-Check (Plagiats-Suche bei ≥ 5 Zeilen,
  Tool-Reference-Filter aktiv); SPDX-License-Identifier in Quelldateien;
  Lizenz-Matrix `LICENSE-COMPATIBILITY.md`; SBOM mit `licenseId` für
  jede Abhängigkeit.
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

#### CL-09-07: Datenschutz in Prompts / Data Protection in Prompts

- **DE:** Personenbezogene Daten, Geheimnisse, Tokens,
  Kundenzeichnungen oder vertrauliche Konfigurationen gehören nicht in
  Prompts an externe KI-Dienste, weil das Senden eine Übermittlung an
  einen Dritten gemäß DSGVO Art. 4(2) und ggf. eine Drittlandübermittlung
  Art. 44–46 darstellt und den DPA-Vertragsumfang sprengen kann.
  Verbotene Inhalte in Prompts: (a) Klartext-Passwörter, API-Tokens
  (AWS Access Keys `AKIA[0-9A-Z]{16}`, GitHub PAT `ghp_*`/`gho_*`,
  Slack `xox[baprs]-*`, OpenAI `sk-*`, Stripe `sk_live_*`, Twilio AC*,
  generic `Bearer eyJ...`-Tokens), Datenbank-Connection-Strings mit
  Credentials, private SSH-Keys (`-----BEGIN OPENSSH PRIVATE KEY-----`,
  `-----BEGIN RSA PRIVATE KEY-----`), TLS-Privatschlüssel; (b)
  personenbezogene Daten DSGVO Art. 4(1) — Namen + E-Mail + Adresse +
  Telefon + Geburtsdatum + IBAN + Steuer-ID + Sozialversicherungsnummer
  + IP-Adressen mit Personenbezug + Standortdaten; (c) besondere
  Datenkategorien Art. 9 — Gesundheits-, Religions-, Gewerkschafts-,
  ethnische, biometrische, sexuelle Daten (zusätzlich strengere
  Anforderungen); (d) vertrauliche Geschäftsdaten — interne
  Architekturdiagramme mit Secrets, unveröffentlichte
  Sicherheitslücken/CVE-Drafts, M&A-Inhalte, Kundenverträge,
  Tariflohn-Daten, interne Strategiedokumente; (e) regulatorisch
  geschützte Daten — PCI-DSS-Karteninhaber-Daten (PAN, CVV2),
  HIPAA-PHI (Patient Health Information), KRITIS-Detail-Topologien.
  Konkrete Schutzmaßnahmen: (1) Pre-Commit-Hook mit gitleaks,
  detect-secrets, trufflehog (siehe CL_Sichere-Entwicklungsumgebung Item
  6); (2) IDE-Plugin-Härtung — GitHub Copilot Business
  `excludeFiles`-Profil in `.copilotignore` oder `.github/copilot.yml`
  ausschließt: `*.env*`, `**/secrets/**`, `**/.aws/**`, `**/.ssh/**`,
  `**/*.pem`, `**/*.p12`, `**/credentials.json`, `**/private/**`; (3)
  DLP (Data Loss Prevention) am Netzwerk — Netskope, Microsoft Purview
  DLP, Forcepoint, Symantec DLP — blockiert Prompts mit erkannten
  Mustern an `*.openai.com`, `*.anthropic.com`, `*.copilot.github.com`;
  (4) Lokale LLM-Alternative für sensible Daten (Continue.dev mit
  Ollama, vLLM, Tabby Self-Hosted) statt Cloud-Modell; (5) Schulung
  „Was darf ich in Prompts schreiben?" mit konkreten Beispielen vor
  Tool-Aktivierung. (6) Anonymisierung-Vorprozess: Hashen statt
  einfügen — Hash-Funktion zu Beispiel-Daten, Faker-Bibliotheken für
  realistische aber synthetische Beispiele. Logging und Audit:
  Cloud-LLM-Audit-Logs (Copilot Audit Log, Claude Console, ChatGPT
  Enterprise Compliance API) wöchentlich auf Anomalien prüfen. Vorfall-
  Reaktion: Bei versehentlichem Prompt-Leak — sofort Token rotieren
  (alle exposed credentials), Vendor kontaktieren mit Löschungsantrag
  DSGVO Art. 17, Vorfall in Sicherheits-Register dokumentieren, ggf.
  Meldung an Datenschutzbehörde DSGVO Art. 33 binnen 72 h.
  Referenzen: DSGVO Art. 4/6/9/25/28/32/33/44, OWASP LLM Top 10 LLM02
  (Sensitive Information Disclosure), OWASP LLM Top 10 LLM06 (Excessive
  Agency), NIST AI RMF MAP-1.6 (Privacy), ISO/IEC 27018 (Privacy in
  Cloud), BSI TR-03161, EU AI Act Verordnung (EU) 2024/1689 Art. 10
  (Data Governance).
- **EN:** Personal data, secrets, tokens, customer identifiers, or
  confidential configuration must not appear in prompts to external AI
  services, because sending constitutes a transfer to a third party
  under GDPR Art. 4(2) and potentially a third-country transfer
  Art. 44–46, and may exceed the DPA contract scope. Forbidden prompt
  content: (a) plaintext passwords, API tokens (AWS access keys
  `AKIA[0-9A-Z]{16}`, GitHub PAT `ghp_*`/`gho_*`, Slack
  `xox[baprs]-*`, OpenAI `sk-*`, Stripe `sk_live_*`, Twilio AC*,
  generic `Bearer eyJ...` tokens), database connection strings with
  credentials, private SSH keys (`-----BEGIN OPENSSH PRIVATE KEY-----`,
  `-----BEGIN RSA PRIVATE KEY-----`), TLS private keys; (b) personal
  data per GDPR Art. 4(1) — names + email + address + phone + birthdate
  + IBAN + tax ID + social security number + IP addresses with personal
  reference + location data; (c) special categories Art. 9 — health,
  religion, union, ethnic, biometric, sexual data (additional stricter
  requirements); (d) confidential business data — internal architecture
  diagrams with secrets, unpublished vulnerabilities/CVE drafts, M&A
  content, customer contracts, salary data, internal strategy documents;
  (e) regulatory protected data — PCI-DSS cardholder data (PAN, CVV2),
  HIPAA PHI (Patient Health Information), KRITIS detailed topologies.
  Concrete protection measures: (1) pre-commit hook with gitleaks,
  detect-secrets, trufflehog (see CL_Sichere-Entwicklungsumgebung item
  6); (2) IDE plugin hardening — GitHub Copilot Business
  `excludeFiles` profile in `.copilotignore` or `.github/copilot.yml`
  excludes: `*.env*`, `**/secrets/**`, `**/.aws/**`, `**/.ssh/**`,
  `**/*.pem`, `**/*.p12`, `**/credentials.json`, `**/private/**`; (3)
  network DLP (Data Loss Prevention) — Netskope, Microsoft Purview
  DLP, Forcepoint, Symantec DLP — blocks prompts with detected patterns
  to `*.openai.com`, `*.anthropic.com`, `*.copilot.github.com`; (4)
  local LLM alternative for sensitive data (Continue.dev with Ollama,
  vLLM, Tabby Self-Hosted) instead of cloud model; (5) training "What
  may I write in prompts?" with concrete examples before tool
  activation. (6) anonymization preprocess: hash instead of paste —
  hash function over example data, Faker libraries for realistic but
  synthetic examples. Logging and audit: weekly review of cloud LLM
  audit logs (Copilot Audit Log, Claude Console, ChatGPT Enterprise
  Compliance API) for anomalies. Incident response: on accidental
  prompt leak — immediately rotate tokens (all exposed credentials),
  contact vendor with GDPR Art. 17 deletion request, document incident
  in security register, possibly notify supervisory authority within 72
  h per GDPR Art. 33. References: GDPR Art. 4/6/9/25/28/32/33/44,
  OWASP Top 10 for LLM Applications LLM02:2025 (Sensitive Information Disclosure), OWASP LLM
  Top 10 LLM06 (Excessive Agency), NIST AI RMF MAP-1.6 (Privacy),
  ISO/IEC 27018 (Privacy in Cloud), BSI TR-03161, EU AI Act Regulation
  (EU) 2024/1689 Art. 10 (Data Governance).
- **Akzeptanz / Acceptance:** Schulung absolviert mit Beispielkatalog;
  Pre-Commit-Schutz (gitleaks/detect-secrets) aktiv; `excludeFiles`-Profil
  in IDE-Konfiguration; DLP für Cloud-LLM-Endpunkte aktiv; Audit-Log-Review
  wöchentlich; Incident-Response-Plan für Prompt-Leak.
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

#### CL-09-08: Keine eigene Krypto durch KI / No Custom Crypto from AI

- **DE:** Die KI darf keine eigenen Krypto-Implementierungen
  vorschlagen, weil LLMs typischerweise alte oder fehlerhafte Beispiele
  reproduzieren (statisch initialisierte IVs, fehlende Authentifizierung,
  Padding-Oracle-Anfälligkeit, fehlende Constant-Time-Operationen,
  schwache Key-Derivation, ECB-Modus, MD5/SHA-1 für Signaturen). Es
  werden bewährte Bibliotheken verwendet (siehe
  „CL_Krypto-Mindestvorgaben"). Anti-Patterns, die KIs häufig
  produzieren und die Reviewer aktiv ablehnen: (a) eigene
  AES-Implementierung in der Anwendung („educational AES"-Code), (b)
  RSA `Cipher.getInstance("RSA")` ohne Padding (PKCS1Padding ist
  unsicher gegen Bleichenbacher → OAEP zwingend), (c)
  `MessageDigest.getInstance("MD5")` für Passwörter (statt Argon2id), (d)
  `new SecretKeySpec("hardcodedkey".getBytes(), "AES")` (hardcodierte
  Schlüssel), (e) `Random` statt `SecureRandom`, (f) `==` für
  Hash-Vergleiche statt Constant-Time-Vergleich (Java
  `MessageDigest.isEqual`, .NET `CryptographicOperations.FixedTimeEquals`,
  Python `hmac.compare_digest`, Go `subtle.ConstantTimeCompare`), (g)
  ECB-Modus, (h) AES-CBC ohne MAC oder ohne korrekte
  Authenticated-Encryption (AES-GCM/AES-CCM/ChaCha20-Poly1305 zwingend),
  (i) statisch initialisierte IV/Nonce, (j) selbst implementiertes JWT
  ohne Bibliothek, (k) `crypto.randomBytes(16)` als Passwort-Hash
  (kein KDF), (l) eigene Padding-Implementierung. Erlaubte Bibliotheken
  (siehe CL_Krypto-Mindestvorgaben Item 14): Java — `javax.crypto` mit
  Bouncy Castle, Google Tink, JCA/JCE Sun-Provider; .NET —
  `System.Security.Cryptography` (.NET 8+) und Konscious.Argon2 für
  Passwörter; Python — `cryptography` (PyCA), `pynacl` (libsodium),
  `argon2-cffi`; Go — `golang.org/x/crypto/...`, Standard `crypto/...`
  (außer abgekündigte Funktionen); JavaScript/TypeScript — Web Crypto
  API `crypto.subtle`, `node:crypto`, `libsodium-wrappers`; Rust —
  `ring`, `RustCrypto/*` (z. B. `aes-gcm`, `chacha20poly1305`),
  `rustls`. Reviewer-Pflichten: jede neue Verwendung kryptografischer
  Primitive im Diff markieren, gegen NIST SP 800-131A Rev. 2 und BSI
  TR-02102-1 abgleichen, Hardcoded-Key-Detektion via Semgrep
  (`p/secrets`, `p/owasp-top-ten`, `p/insecure-transport`),
  CodeQL-Queries (`security/cwe-327`, `security/cwe-329`,
  `security/cwe-330`, `security/cwe-916`), Snyk Code, SonarQube
  Security Hotspots. Kryptografie-Code im PR braucht erkennbare
  Bibliotheks-Aufrufe, keine eigene Bit-Operation. Bei legitimen
  Sonderfällen (HSM-Integration, FIPS-zertifizierte Module): explizite
  S-ADR (Security Architecture Decision Record) mit Begründung,
  Cryptographic Officer Review, dokumentierte Schlüsselverwaltung gemäß
  NIST SP 800-57. Referenzen: NIST SP 800-131A Rev. 2, NIST SP 800-57
  Part 1 Rev. 5, BSI TR-02102-1, BSI TR-02102-2, OWASP ASVS V6
  (Stored Cryptography), OWASP Cheat Sheet Cryptographic Storage,
  OWASP Cheat Sheet Key Management, ISO/IEC 27001 A.8.24
  (Cryptographic controls), CRA Anhang I Teil II 2.1 lit. e (Schutz
  durch Verschlüsselung).
- **EN:** The AI must not suggest custom crypto, because LLMs typically
  reproduce old or flawed examples (static IVs, missing authentication,
  padding oracle vulnerability, missing constant-time operations, weak
  key derivation, ECB mode, MD5/SHA-1 for signatures). Use vetted
  libraries (see "CL_Krypto-Mindestvorgaben"). Anti-patterns AIs
  frequently produce and reviewers must actively reject: (a) own AES
  implementation in the application ("educational AES" code), (b) RSA
  `Cipher.getInstance("RSA")` without padding (PKCS1Padding is insecure
  against Bleichenbacher → OAEP mandatory), (c)
  `MessageDigest.getInstance("MD5")` for passwords (instead of
  Argon2id), (d) `new SecretKeySpec("hardcodedkey".getBytes(), "AES")`
  (hardcoded keys), (e) `Random` instead of `SecureRandom`, (f) `==`
  for hash comparisons instead of constant-time comparison (Java
  `MessageDigest.isEqual`, .NET `CryptographicOperations.FixedTimeEquals`,
  Python `hmac.compare_digest`, Go `subtle.ConstantTimeCompare`), (g)
  ECB mode, (h) AES-CBC without MAC or without proper authenticated
  encryption (AES-GCM/AES-CCM/ChaCha20-Poly1305 mandatory), (i)
  statically initialized IV/nonce, (j) self-implemented JWT without
  library, (k) `crypto.randomBytes(16)` as password hash (no KDF), (l)
  own padding implementation. Allowed libraries (see
  CL_Krypto-Mindestvorgaben item 14): Java — `javax.crypto` with Bouncy
  Castle, Google Tink, JCA/JCE Sun provider; .NET —
  `System.Security.Cryptography` (.NET 8+) and Konscious.Argon2 for
  passwords; Python — `cryptography` (PyCA), `pynacl` (libsodium),
  `argon2-cffi`; Go — `golang.org/x/crypto/...`, standard `crypto/...`
  (except deprecated functions); JavaScript/TypeScript — Web Crypto API
  `crypto.subtle`, `node:crypto`, `libsodium-wrappers`; Rust — `ring`,
  `RustCrypto/*` (e.g., `aes-gcm`, `chacha20poly1305`), `rustls`.
  Reviewer duties: flag every new use of cryptographic primitives in
  the diff, compare against NIST SP 800-131A Rev. 2 and BSI TR-02102-1,
  hardcoded key detection via Semgrep (`p/secrets`, `p/owasp-top-ten`,
  `p/insecure-transport`), CodeQL queries (`security/cwe-327`,
  `security/cwe-329`, `security/cwe-330`, `security/cwe-916`), Snyk
  Code, SonarQube security hotspots. Crypto code in the PR needs
  recognisable library calls, not own bit operations. For legitimate
  exceptions (HSM integration, FIPS-certified modules): explicit S-ADR
  (Security Architecture Decision Record) with justification,
  cryptographic officer review, documented key management per NIST SP
  800-57. References: NIST SP 800-131A Rev. 2, NIST SP 800-57 Part 1
  Rev. 5, BSI TR-02102-1, BSI TR-02102-2, OWASP ASVS V6 (Stored
  Cryptography), OWASP Cheat Sheet Cryptographic Storage, OWASP Cheat
  Sheet Key Management, ISO/IEC 27001 A.8.24 (cryptographic controls),
  CRA Annex I Part II 2.1 lit. e (protection by encryption).
- **Akzeptanz / Acceptance:** Review prüft jede Krypto-Codestelle aus
  KI-Quelle gegen erlaubte Bibliotheken; SAST-Gate (Semgrep/CodeQL/Snyk
  Code) ohne Hardcoded-Key/Weak-Algo-Findings; S-ADR bei Sonderfällen
  vorhanden; keine eigenen AES/Hash-Implementierungen.
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

#### CL-09-09: Tests für KI-erzeugten Code / Tests for AI-Generated Code

- **DE:** KI-erzeugter Code wird durch automatisierte Tests abgedeckt
  (positive und negative Fälle). Eine erfolgreiche Pipeline ist
  Voraussetzung für den Merge. Test-Pflicht-Klassen: (a) Unit-Tests für
  jede neue Funktion oder Methode mit mindestens 80 % oder besser Line
  Coverage und mindestens 80 % oder besser Branch Coverage; (b) negative
  Tests für jede Eingabe-Schranke (null,
  empty, oversized, malformed UTF-8, NUL-byte, path traversal `../`,
  XSS-Payloads `<script>`, SQL-Injection `' OR '1'='1`,
  XXE-Payloads, deserialization gadgets); (c) Property-based Tests
  (jqwik Java, hypothesis Python, fast-check JavaScript, proptest Rust,
  QuickCheck Haskell) für komplexe Eingaberäume; (d) Mutation-Testing
  zur Aufdeckung tautologischer Tests, wenn es technisch sinnvoll und im
  Projektwerkzeug verfügbar ist (PIT für Java, Stryker für JavaScript,
  mutmut für Python, go-mutesting für Go) — Mutation-Score mindestens
  70 % oder besser; (e) Sicherheitsspezifische Tests bei Auth/Crypto/Validation:
  OWASP ZAP DAST in CI, Burp Suite Pro für manuelle Pentests vor Major
  Release; (f) Fuzz-Tests bei Parser, Deserialisierung, Datei-Upload —
  libFuzzer (C/C++/Rust), AFL++ (C/C++), Atheris (Python), Jazzer
  (Java), `go test -fuzz` (Go), cargo-fuzz (Rust); (g) Snapshot-Tests
  und Golden-File-Tests für deterministische Ausgaben; (h) bei Änderungen
  an öffentlichen Schnittstellen, REST APIs, CLI-Kommandos oder kritischen
  UI-Flows mindestens 80 % oder besser Integrationstest-Abdeckung der
  dokumentierten betroffenen Schnittstellen und Flows; jede neue oder
  geänderte öffentliche Schnittstelle hat mindestens einen positiven und
  einen negativen Integrationstest; (i)
  Integrations-Tests gegen reale Datenbanken (Testcontainers, no
  mocks-only — vgl. Memory-Eintrag „integration tests must hit a real
  database"). Test-Frameworks je Sprache: Java — JUnit 5 + Mockito +
  AssertJ + Awaitility + Testcontainers; .NET — xUnit + Moq + FluentAssertions
  + Bogus; Python — pytest + pytest-cov + responses + freezegun + faker;
  JavaScript/TypeScript — Vitest/Jest + supertest + msw + faker;
  Go — testing + testify + gomock + testcontainers-go; Rust —
  built-in `#[test]` + proptest + insta; Swift — XCTest + Swift Testing
  (Swift 6); C/C++ — Unity, CMock, Google Test, Catch2. KI-Tests müssen
  insbesondere geprüft werden auf: tautologische Assertions
  (`assertEqual(getValue(), getValue())`), fehlende Edge Cases (null,
  Maximalwerte, leere Listen), keine reine Mock-Verifikation ohne
  Verhaltensprüfung, kein „Test mit gleicher Implementierung wie
  Production-Code". CI-Gate-Pipeline-Beispiel (GitHub Actions):
  ```yaml
  jobs:
    test:
      steps:
        - uses: actions/checkout@v4
        - uses: actions/setup-java@v4
        - run: mvn test
        - run: mvn jacoco:report
        - uses: codecov/codecov-action@v4
        - run: mvn org.pitest:pitest-maven:mutationCoverage
    sast:
      steps:
        - uses: github/codeql-action/init@v3
        - uses: github/codeql-action/analyze@v3
        - uses: returntocorp/semgrep-action@v1
    dast:
      steps:
        - uses: zaproxy/action-baseline@v0
  ```
  Coverage-Berichte in PR (codecov, coveralls, SonarCloud), Coverage-
  Gate `coverage_decrease_threshold: 0.0%`, Mutation-Score-Gate
  mindestens 70 % oder besser, wenn Mutation Testing angewendet wird.
  Referenzen: ISO/IEC 25010 (Quality model), ISO/IEC 29119
  (Software Testing), OWASP Testing Guide v4.2, NIST SP 800-218 PW.7
  (Review and/or Test of Code), CRA Anhang I Teil II 2.5 (Tests vor
  Auslieferung).
- **EN:** AI-generated code is covered by automated tests (positive and
  negative cases). A green pipeline is required before merging.
  Mandatory test classes: (a) unit tests for every new function or
  method with at least 80 % or better line coverage and at least 80 %
  or better branch coverage; (b) negative tests for every input boundary
  (null, empty, oversized,
  malformed UTF-8, NUL byte, path traversal `../`, XSS payloads
  `<script>`, SQL injection `' OR '1'='1`, XXE payloads, deserialization
  gadgets); (c) property-based tests (jqwik Java, hypothesis Python,
  fast-check JavaScript, proptest Rust, QuickCheck Haskell) for complex
  input spaces; (d) mutation testing to detect tautological tests when it
  is technically useful and available in the project tooling (PIT for
  Java, Stryker for JavaScript, mutmut for Python, go-mutesting for Go) —
  mutation score at least 70 % or better; (e) security-specific tests for
  auth/crypto/validation: OWASP ZAP DAST in CI, Burp Suite Pro for
  manual pentests before major release; (f) fuzz tests for parsers,
  deserialization, file upload — libFuzzer (C/C++/Rust), AFL++
  (C/C++), Atheris (Python), Jazzer (Java), `go test -fuzz` (Go),
  cargo-fuzz (Rust); (g) snapshot tests and golden file tests for
  deterministic outputs; (h) for changes to public interfaces, REST APIs,
  CLI commands, or critical UI flows, at least 80 % or better integration
  test coverage of the documented affected interfaces and flows; every
  new or changed public interface has at least one positive and one
  negative integration test; (i) integration tests against real databases
  (Testcontainers, no mocks-only — cf. memory entry "integration tests
  must hit a real database"). Test frameworks by language: Java —
  JUnit 5 + Mockito + AssertJ + Awaitility + Testcontainers; .NET —
  xUnit + Moq + FluentAssertions + Bogus; Python — pytest + pytest-cov
  + responses + freezegun + faker; JavaScript/TypeScript —
  Vitest/Jest + supertest + msw + faker; Go — testing + testify +
  gomock + testcontainers-go; Rust — built-in `#[test]` + proptest +
  insta; Swift — XCTest + Swift Testing (Swift 6); C/C++ — Unity,
  CMock, Google Test, Catch2. AI tests must especially be checked for:
  tautological assertions (`assertEqual(getValue(), getValue())`),
  missing edge cases (null, maximum values, empty lists), no pure mock
  verification without behavioural checks, no "test with the same
  implementation as production code". Example CI gate pipeline (GitHub
  Actions):
  ```yaml
  jobs:
    test:
      steps:
        - uses: actions/checkout@v4
        - uses: actions/setup-java@v4
        - run: mvn test
        - run: mvn jacoco:report
        - uses: codecov/codecov-action@v4
        - run: mvn org.pitest:pitest-maven:mutationCoverage
    sast:
      steps:
        - uses: github/codeql-action/init@v3
        - uses: github/codeql-action/analyze@v3
        - uses: returntocorp/semgrep-action@v1
    dast:
      steps:
        - uses: zaproxy/action-baseline@v0
  ```
  Coverage reports in the PR (codecov, coveralls, SonarCloud), coverage
  gate `coverage_decrease_threshold: 0.0%`, mutation score gate at least
  70 % or better when mutation testing is used.
  References: ISO/IEC 25010 (quality model), ISO/IEC 29119 (software
  testing), OWASP Testing Guide v4.2, NIST SP 800-218 PW.7 (review
  and/or test of code), CRA Annex I Part II 2.5 (tests before
  delivery).
- **Akzeptanz / Acceptance:** Coverage-Wert je betroffenem Modul
  gepflegt mit Line Coverage mindestens 80 % oder besser und Branch
  Coverage mindestens 80 % oder besser; wenn Mutation Testing technisch
  sinnvoll und verfügbar ist, Mutation-Score mindestens 70 % oder besser;
  bei Änderungen an öffentlichen Schnittstellen oder kritischen UI-Flows
  mindestens 80 % oder besser Integrationstest-Abdeckung der dokumentierten
  betroffenen Schnittstellen und Flows; SAST/DAST-Gates grün; negative
  Tests für jede Eingabe-Schranke; keine tautologischen Assertions. /
  Coverage value is maintained per affected module with line coverage at
  least 80 % or better and branch coverage at least 80 % or better; where
  mutation testing is technically useful and available, mutation score at
  least 70 % or better; for changes to public interfaces or critical UI
  flows, at least 80 % or better integration-test coverage of documented
  affected interfaces and flows; SAST/DAST gates are green; negative tests
  cover every input boundary; no tautological assertions.
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

#### CL-09-10: Markierung im PR / Mark in the PR

- **DE:** Ist ein wesentlicher Teil eines Pull Requests durch KI
  vorgeschlagen worden, wird das im PR-Beschreibungstext oder Label
  vermerkt, weil dies für Audit-Spur, Lizenz-Klärung, Reviewer-
  Aufmerksamkeit und Lessons-Learned-Statistik (Anteil KI-generierten
  Codes über Zeit, Bug-Korrelation) wichtig ist. Schwellenwert
  „wesentlich": ≥ 25 % der LoC im PR aus KI-Vorschlägen, oder
  vollständige Implementierung einer Funktion ≥ 20 LoC, oder eine
  Architekturentscheidung aus KI-Empfehlung, oder
  sicherheitsrelevanter Codepfad. Markierungs-Methoden: (a) GitHub-Labels
  `ai-assisted`, `ai-major`, `ai-trivial` definieren in
  `.github/labels.yml` und automatisch via GitHub Actions setzen; (b)
  PR-Template-Pflichtabschnitt:
  ```markdown
  ## AI Assistance / KI-Beteiligung
  - [ ] Kein KI-Beitrag / no AI contribution
  - [ ] Trivial (<25% LoC, IDE-Autocompletion) / trivial completion
  - [ ] Wesentlich / substantial — Tool: <Copilot|Claude|Gemini|...>
        Version: <version>
        Umfang: <Beschreibung in 1–3 Sätzen>
        Manuell zeilenweise geprüft am: <YYYY-MM-DD> von <Reviewer>
  ```
  (c) Commit-Trailer: `Co-developed-with: GitHub-Copilot/4.x` oder
  `Assisted-by: Claude Opus 4.7` analog zu `Co-Authored-By:`-Pattern;
  (d) Code-Kommentar bei umfangreichen KI-generierten Blöcken (≥ 50
  LoC) mit Tool-Vermerk und Review-Datum (sparsam, nur wo Audit-
  Relevanz besteht); (e) Repository-Statistik via Skript (z. B.
  `git log --grep "Co-developed-with" --since=YYYY-MM-DD`) für
  monatlichen Bericht KI-Anteil. Anti-Pattern: KI-Anteil verschweigen,
  „Squashing" zur Verschleierung der KI-Spur, Trailer-Entfernung beim
  Rebase, Standard-Label-Setzung ohne Inhaltsprüfung. Tooling: GitHub
  ProBot mit eigener Action, Git CI-Lint-Check, Pull Request
  Validator (Bot prüft PR-Beschreibung gegen Template) — Fehlend führt
  zu Status-Check „failed". Reporting: monatliches Dashboard für
  Engineering-Lead und Security-Lead mit KI-Anteil pro Repository,
  Bug-Korrelation (CVE/Bug-Findings je Code-Anteil-Klasse),
  Review-Geschwindigkeit. Referenzen: ISO/IEC 27001 A.5.34 (Privacy
  and protection of PII — analoge Transparenz-Pflicht), NIST AI RMF
  GOVERN-1.4 (Transparenz), EU AI Act Art. 50 (Transparenz-Pflichten),
  Linux Foundation Best Practices Generative AI.
- **EN:** When a substantial part of a pull request was AI-suggested,
  this is recorded in the PR description or via a label, because this
  is important for audit trail, licence clearance, reviewer attention,
  and lessons-learned statistics (share of AI-generated code over time,
  bug correlation). "Substantial" threshold: ≥ 25 % of LoC in the PR
  from AI suggestions, or full implementation of a function ≥ 20 LoC,
  or an architecture decision from AI recommendation, or a security-
  relevant code path. Marking methods: (a) define GitHub labels
  `ai-assisted`, `ai-major`, `ai-trivial` in `.github/labels.yml` and
  set automatically via GitHub Actions; (b) mandatory section in the
  PR template:
  ```markdown
  ## AI Assistance / KI-Beteiligung
  - [ ] Kein KI-Beitrag / no AI contribution
  - [ ] Trivial (<25% LoC, IDE autocompletion) / trivial completion
  - [ ] Wesentlich / substantial — Tool: <Copilot|Claude|Gemini|...>
        Version: <version>
        Scope: <description in 1–3 sentences>
        Manually reviewed line by line on: <YYYY-MM-DD> by <reviewer>
  ```
  (c) commit trailer: `Co-developed-with: GitHub-Copilot/4.x` or
  `Assisted-by: Claude Opus 4.7`, analogous to the `Co-Authored-By:`
  pattern; (d) code comment on extensive AI-generated blocks (≥ 50
  LoC) with tool note and review date (sparingly, only where audit
  relevance exists); (e) repository statistics via script (e.g., `git
  log --grep "Co-developed-with" --since=YYYY-MM-DD`) for monthly AI
  share report. Anti-pattern: hiding AI involvement, "squashing" to
  obscure the AI trail, trailer removal during rebase, default label
  setting without content review. Tooling: GitHub ProBot with custom
  action, Git CI lint check, Pull Request Validator (bot checks PR
  description against template) — missing results in "failed" status
  check. Reporting: monthly dashboard for engineering lead and security
  lead with AI share per repository, bug correlation (CVE/bug findings
  per code share class), review speed. References: ISO/IEC 27001
  A.5.34 (privacy and protection of PII — analogous transparency
  obligation), NIST AI RMF GOVERN-1.4 (transparency), EU AI Act Art. 50
  (transparency obligations), Linux Foundation Best Practices for
  Generative AI.
- **Akzeptanz / Acceptance:** Label `ai-assisted`/`ai-major`/`ai-trivial`
  oder Beschreibungstext mit Tool, Version, Umfang, Reviewer und
  Review-Datum sichtbar; PR-Validator-Bot prüft Pflichtabschnitt;
  Commit-Trailer `Co-developed-with`/`Assisted-by` bei wesentlichen
  Beiträgen; monatliches Dashboard.
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

#### CL-09-11: Werkzeug-Konfiguration und Telemetrie / Tool Configuration and Telemetry

- **DE:** Telemetrie und Daten-Sharing der KI-Werkzeuge sind nach
  Vorgabe konfiguriert. Sensible Code-Bereiche werden vom KI-Werkzeug
  per Konfiguration ausgenommen, weil Cloud-Werkzeuge je nach Tier
  Telemetrie an den Anbieter senden (Latenz-Metriken, Akzeptanz-Raten,
  Fehler-Logs) und manche zusätzlich Code-Snippets für Modell-
  Verbesserung. Konkrete Konfigurations-Profile je Werkzeug:

  **GitHub Copilot Business/Enterprise** — Org-Admin-Settings:
  - Suggestions matching public code: `Block`
  - Allow GitHub to use my code snippets for product improvements:
    `false` (default in Business/Enterprise, in Individual variabel)
  - Content exclusions in `.copilotignore` oder via Org-Settings
    (Repo-Pfade): `*.env*`, `**/secrets/**`, `**/.aws/**`, `**/.ssh/**`,
    `**/*.pem`, `**/*.p12`, `**/*.pfx`, `**/credentials.json`,
    `**/*.kdbx`, `**/.gnupg/**`, `**/security/**`, `**/private/**`,
    `**/personal-data/**`
  - VS-Code-Setting: `"github.copilot.advanced": { "secret_key": "..." }`
    nicht einrichten; `"github.copilot.editor.enableAutoCompletions":
    true` aktiviert lassen
  - JetBrains Plugin: Settings → GitHub Copilot → Block public code:
    enabled
  - Audit Log via GitHub REST API
    `/orgs/{org}/copilot/usage` und Compliance API

  **JetBrains AI Assistant** — Settings → Tools → AI Assistant:
  - „Allow detail data collection": disabled
  - „Connect to local model only" wenn DSGVO-kritisch
  - Codebase-Indexing-Ausschluss-Patterns analog `excludeFiles`

  **Anthropic Claude (Code/Console)** — Workspace-Settings:
  - Zero Data Retention (ZDR) aktiviert (Enterprise-Verhandlung
    erforderlich)
  - Trainings-Opt-out aktiviert
  - System-Prompt: „Do not access files in /secrets, /.env, /.aws,
    /.ssh"

  **OpenAI ChatGPT Enterprise/Team** — Admin-Console:
  - „Use my data for training": disabled
  - SOC 2 Type II + ISO/IEC 27001-Nachweis vorhanden
  - SSO/SAML mit IdP-Conditional-Access

  **AWS Q Developer Pro** — IAM-Identity-Center-Setup:
  - Customizations: blockierte Repos via Allowlist
  - Reference tracker: enabled

  **Continue.dev (lokal)** — `~/.continue/config.yaml`:
  - Provider: `ollama` mit `localhost:11434`
  - Telemetry: disabled (`allowAnonymousTelemetry: false`)
  - System-Prompt mit Pfad-Ausschluss

  **Tabnine** — Self-Hosted oder Enterprise mit zero retention.

  Versioniertes Konfigurations-Profil in Repo: `.github/copilot.yml`
  oder `.copilotignore`/`.aiderignore`/`.cursorignore` je Werkzeug,
  zusätzlich Org-weit dokumentiert in `docs/security/ai-tool-config.md`
  mit Owner, letztem Review-Datum und Drift-Detection-Skript (Vergleich
  Org-API-Settings vs. Soll-Zustand). Audit: monatlicher Vergleich der
  Live-Konfiguration gegen Soll mittels GitHub REST/JetBrains-Audit/
  Anthropic-Console-API; Abweichung erzeugt Ticket. Referenzen: DSGVO
  Art. 25 (Privacy by Design and by Default), Art. 32 (Sicherheit der
  Verarbeitung), ISO/IEC 27001 A.5.23 (Cloud-Services), A.8.10
  (Information deletion), NIST AI RMF MEASURE-2.10 (data privacy
  measurement), EU AI Act Art. 10 (Data Governance).
- **EN:** Telemetry and data sharing of AI tools are configured per
  policy. Sensitive code areas are excluded from the AI tool by
  configuration, because cloud tools send telemetry to the vendor
  depending on tier (latency metrics, acceptance rates, error logs)
  and some additionally send code snippets for model improvement.
  Concrete configuration profiles per tool:

  **GitHub Copilot Business/Enterprise** — org admin settings:
  - Suggestions matching public code: `Block`
  - Allow GitHub to use my code snippets for product improvements:
    `false` (default in Business/Enterprise, variable in Individual)
  - Content exclusions in `.copilotignore` or via org settings (repo
    paths): `*.env*`, `**/secrets/**`, `**/.aws/**`, `**/.ssh/**`,
    `**/*.pem`, `**/*.p12`, `**/*.pfx`, `**/credentials.json`,
    `**/*.kdbx`, `**/.gnupg/**`, `**/security/**`, `**/private/**`,
    `**/personal-data/**`
  - VS Code setting: do not configure
    `"github.copilot.advanced": { "secret_key": "..." }`; leave
    `"github.copilot.editor.enableAutoCompletions": true`
  - JetBrains plugin: Settings → GitHub Copilot → Block public code:
    enabled
  - Audit log via GitHub REST API
    `/orgs/{org}/copilot/usage` and Compliance API

  **JetBrains AI Assistant** — Settings → Tools → AI Assistant:
  - "Allow detail data collection": disabled
  - "Connect to local model only" when GDPR-critical
  - Codebase indexing exclusion patterns analogous to `excludeFiles`

  **Anthropic Claude (Code/Console)** — workspace settings:
  - Zero Data Retention (ZDR) enabled (enterprise negotiation
    required)
  - Training opt-out enabled
  - System prompt: "Do not access files in /secrets, /.env, /.aws,
    /.ssh"

  **OpenAI ChatGPT Enterprise/Team** — admin console:
  - "Use my data for training": disabled
  - SOC 2 Type II + ISO/IEC 27001 certificate available
  - SSO/SAML with IdP conditional access

  **AWS Q Developer Pro** — IAM Identity Center setup:
  - Customizations: blocked repos via allowlist
  - Reference tracker: enabled

  **Continue.dev (local)** — `~/.continue/config.yaml`:
  - Provider: `ollama` with `localhost:11434`
  - Telemetry: disabled (`allowAnonymousTelemetry: false`)
  - System prompt with path exclusion

  **Tabnine** — self-hosted or enterprise with zero retention.

  Versioned configuration profile in repo: `.github/copilot.yml` or
  `.copilotignore`/`.aiderignore`/`.cursorignore` per tool,
  additionally org-wide documented in `docs/security/ai-tool-config.md`
  with owner, last review date, and drift detection script
  (comparison of org API settings vs. target state). Audit: monthly
  comparison of live configuration vs. target via GitHub REST/JetBrains
  audit/Anthropic Console API; deviation creates a ticket. References:
  GDPR Art. 25 (Privacy by Design and by Default), Art. 32 (Security
  of Processing), ISO/IEC 27001 A.5.23 (cloud services), A.8.10
  (information deletion), NIST AI RMF MEASURE-2.10 (data privacy
  measurement), EU AI Act Art. 10 (Data Governance).
- **Akzeptanz / Acceptance:** Konfigurations-Profil im Repository
  (`.copilotignore`/`.github/copilot.yml`/`.continue/config.yaml`); Org-
  Admin-Settings für Trainings-Opt-out und Public-Code-Block aktiv;
  monatlicher Drift-Audit dokumentiert; Owner und Review-Datum in
  `docs/security/ai-tool-config.md`.
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

#### CL-09-12: Schulung / Training

- **DE:** Entwicklerinnen und Entwickler erhalten eine Schulung zu Stärken, Schwächen
  und Risiken von KI-Code-Assistenten. Auffrischung mindestens jährlich.
  Schulungs-Curriculum: (a) Stärken — Beschleunigung von Boilerplate,
  Test-Skelette, Doku-Generierung, Refactoring-Vorschläge,
  Sprach-/Framework-Onboarding, Code-Erklärung; (b) Schwächen —
  Halluzinationen (erfundene APIs, falsche Bibliotheksnamen, falsche
  Versions-Annahmen), veraltete Trainingsdaten (typischer Cut-off Monate
  bis Jahre vor Modell-Release), Übergeneralisierung („Standardlösung"
  passt nicht zum Kontext), tautologische Tests, unsicherer Default-
  Code (siehe Item 8 — Krypto-Anti-Patterns); (c) Risiken — Slopsquatting
  (Item 5), Lizenz-Probleme (Item 6), Prompt-Datenleck (Item 7),
  Übervertrauen (NIST AI RMF MEASURE-2.7), schwächere
  Programmier-Fertigkeiten bei Junior-Entwicklern bei Dauernutzung
  („skill atrophy", Stanford 2024), Verzerrungen aus Trainingsdaten,
  Halluzinations-Wiederholungen bei iterativen Prompts; (d) Praktiken
  — präzise Prompts mit Kontext, Context-Window-Management, manuelle
  zeilenweise Prüfung, Kreuz-Verifikation gegen offizielle Dokumentation,
  Schulungs-Übungen mit absichtlich falschen KI-Vorschlägen
  („Spot-the-Bug"-Übungen). Empfohlene Schulungsformate: 4-Stunden-
  Onboarding (mit Praxisteil zu allen Items dieser Checkliste),
  jährliche Auffrischung 2 h, Just-in-Time-Mikro-Trainings bei neuen
  Tool-Versionen oder bekannten KI-Vorfällen (z. B. „Slopsquatting-
  Vorfall huggingface-cli"), Capture-the-Flag mit absichtlich
  fehlerhaften KI-Vorschlägen halbjährlich. Schulungs-Plattformen:
  SecureFlag (Modul „AI-Assisted Development"), Kontra Application
  Security, GitHub Skills (kostenlose „Copilot for Pull Requests"-
  Pfade), Anthropic Skill-Quests (kostenlos), interne Workshops mit
  Security Champions. Empfohlene Lektüre: OWASP Top 10 for LLM
  Applications (`genai.owasp.org/llm-top-10`), Stanford Study „Do
  Users Write More Insecure Code with AI Assistants?" (Perry et al.
  2023), GitHub Octoverse Reports zu Copilot-Adoption und Bug-Korrelation.
  Nachweisführung: Schulungsmatrix in HR/LMS (Workday, SAP
  SuccessFactors, Cornerstone OnDemand, Moodle, TalentLMS), automatische
  Eskalation bei Frist-Überschreitung (30-Tage-Nachfrist, dann
  Sperrung des KI-Tool-Zugriffs via Microsoft Entra ID/Okta SCIM).
  Onboarding-Pflicht: KI-Tool-Zugriff erst nach abgeschlossener
  Schulung. Referenzen: NIST SP 800-50 (Building IT Security Awareness
  and Training Program), NIST SP 800-181 (NICE Workforce Framework),
  NIST AI RMF MAP-3.5 (skill awareness), ISO/IEC 27001 A.6.3 (Awareness,
  Education, Training), BSI ORP.3, EU AI Act Art. 4 (AI Literacy).
- **EN:** Developers receive training on strengths, weaknesses, and
  risks of AI code assistants. Refreshed at least once a year. Training
  curriculum: (a) strengths — boilerplate acceleration, test
  skeletons, documentation generation, refactoring suggestions,
  language/framework onboarding, code explanation; (b) weaknesses —
  hallucinations (invented APIs, wrong library names, wrong version
  assumptions), outdated training data (typical cut-off months to
  years before model release), overgeneralization ("standard solution"
  does not fit the context), tautological tests, unsafe default code
  (see item 8 — crypto anti-patterns); (c) risks — slopsquatting (item
  5), licence issues (item 6), prompt data leakage (item 7), over-
  reliance (NIST AI RMF MEASURE-2.7), weaker programming skills in
  junior developers under continuous use ("skill atrophy", Stanford
  2024), biases from training data, hallucination repetition under
  iterative prompts; (d) practices — precise prompts with context,
  context window management, manual line-by-line review, cross-
  verification against official documentation, training exercises with
  intentionally wrong AI suggestions ("spot the bug" exercises).
  Recommended training formats: 4-hour onboarding (with hands-on for
  all items in this checklist), annual refresher 2 h, just-in-time
  micro trainings on new tool versions or known AI incidents (e.g.,
  "slopsquatting incident huggingface-cli"), capture-the-flag with
  intentionally flawed AI suggestions semi-annually. Training
  platforms: SecureFlag (module "AI-Assisted Development"), Kontra
  Application Security, GitHub Skills (free "Copilot for Pull
  Requests" paths), Anthropic Skill Quests (free), internal workshops
  with security champions. Recommended reading: OWASP Top 10 for LLM
  Applications (`genai.owasp.org/llm-top-10`), Stanford Study "Do
  Users Write More Insecure Code with AI Assistants?" (Perry et al.
  2023), GitHub Octoverse reports on Copilot adoption and bug
  correlation. Proof: training matrix in HR/LMS (Workday, SAP
  SuccessFactors, Cornerstone OnDemand, Moodle, TalentLMS), automatic
  escalation on deadline overrun (30-day grace period, then AI tool
  access suspension via Microsoft Entra ID/Okta SCIM). Onboarding
  obligation: AI tool access only after completed training.
  References: NIST SP 800-50 (Building IT Security Awareness and
  Training Program), NIST SP 800-181 (NICE Workforce Framework), NIST
  AI RMF MAP-3.5 (skill awareness), ISO/IEC 27001 A.6.3 (awareness,
  education, training), BSI ORP.3, EU AI Act Art. 4 (AI Literacy).
- **Akzeptanz / Acceptance:** Schulungsnachweis pro Person in HR/LMS
  vorhanden; Onboarding vor erstem KI-Tool-Zugriff abgeschlossen;
  jährliche Auffrischung dokumentiert; CTF/Spot-the-Bug-Übungen
  halbjährlich; Erfüllungsquote ≥ 95 % im Quartalsbericht.
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

#### CL-09-13: Audit-Spur der Werkzeugnutzung / Audit Trail of Tool Usage

- **DE:** Die Nutzung der KI-Werkzeuge ist nachvollziehbar
  (Werkzeugname, Zeitraum, beteiligte Personen). Zentrale Logs des
  Werkzeugs werden für Audits aufbewahrt, soweit möglich. Konkrete
  Audit-Quellen je Werkzeug:

  **GitHub Copilot Business/Enterprise:** REST-API
  `/orgs/{org}/copilot/usage` (täglich aggregierte Nutzungsmetriken),
  `/enterprises/{enterprise}/copilot/usage`, Audit Log API
  `/orgs/{org}/audit-log` mit Action-Patterns
  `copilot.cfb_seat_*`/`copilot_user_*`, Compliance API für PR-bezogene
  Suggestions-Akzeptanz, GitHub Webhook auf
  `copilot_seat_management`-Events. Logs in eigenen S3-Bucket via
  GitHub Actions oder direkt in SIEM (Splunk, Microsoft Sentinel,
  Elastic) replizieren.

  **JetBrains AI Assistant:** Audit-Events in JetBrains Toolbox
  Enterprise Console; lokale Logs in `~/Library/Logs/JetBrains/...`
  (macOS) bzw. `%APPDATA%/JetBrains/...` (Windows) bei Bedarf
  per MDM einsammeln.

  **Anthropic Claude Console (Workspaces):** Console → Settings →
  Activity Logs (90-180 Tage Retention), Audit-Log-Export via API,
  Webhooks für Workspace-Events; `claude code`-CLI lokal:
  `.claude/projects/<repo-hash>/*.jsonl` lokal vorhanden, optional
  zentralisierbar.

  **OpenAI ChatGPT Enterprise/Team:** Compliance API für Conversations,
  Workspace Activity Logs, SIEM-Integration via API-Polling.

  **AWS Q Developer Pro:** AWS CloudTrail-Events, IAM Identity Center
  Access Logs, Q Developer Customizations Audit.

  **Git Duo:** Git Audit Events Stream (`Audit Streaming`-Feature
  mit JSONL-Export an SIEM-Endpunkt), Project-Level-Events.

  **Continue.dev/Tabby/Ollama lokal:** `~/.continue/sessions/*.json`,
  Ollama-Logs in `journalctl -u ollama` oder
  `~/Library/Logs/Ollama/server.log`.

  Aufbewahrungsfristen-Pflicht: ≥ 1 Jahr für Standard-Audits, ≥ 6
  Jahre für DSGVO-Compliance-Bezüge, ≥ 10 Jahre für CRA-Vorfall-
  Verläufe (Verordnung (EU) 2024/2847 Art. 14). Speicherort: zentraler
  SIEM (Splunk Enterprise Security mit `audit_logs`-Index, Microsoft
  Sentinel Workspace mit DCR-Rules, Elastic Security mit dedicated
  data stream, IBM QRadar AQL-Query-Repository), append-only mit WORM
  (S3 Object Lock compliance mode, Azure Immutable Blob Storage with
  Time-based Retention, Google Cloud Storage Bucket Lock). Zugriffs-
  Modell: nur Security-Team und Compliance-Officer (RBAC), Vier-Augen
  für Löschungen vor Ablauf der Retention. Korrelation: KI-Tool-Logs
  mit Git-Commit-Logs (via `Co-developed-with`-Trailer aus Item 10)
  und PR-Metadaten matchen, um KI-Anteil je Vorfall zu bestimmen.
  Beispiel-SIEM-Query (Splunk):
  ```text
  index=audit_logs sourcetype=github_audit
  action=copilot_user.* user=<dev>
  | stats count by action _time
  ```
  Datenschutz-Konformität: Logs enthalten typischerweise Username/E-Mail
  → in Verzeichnis von Verarbeitungstätigkeiten (DSGVO Art. 30) als
  Verarbeitung „Audit-Logging KI-Tool-Nutzung" eintragen, technische
  und organisatorische Maßnahmen Art. 32 dokumentieren, Betriebsrat-
  Information bei Mitbestimmungsrecht (BetrVG § 87 Abs. 1 Nr. 6 in DE).
  Referenzen: ISO/IEC 27001 A.8.15 (Logging) und A.5.34 (Privacy and
  protection of PII), NIST SP 800-92 (Log Management), DSGVO Art. 30,
  CRA Anhang I Teil II 2.1 lit. c (Logging), BSI IT-Grundschutz
  OPS.1.1.5 (Protokollierung), EU AI Act Art. 12 (Record-keeping).
- **EN:** AI tool use is traceable (tool name, time frame, people
  involved). Where feasible, central tool logs are retained for
  audits. Concrete audit sources per tool:

  **GitHub Copilot Business/Enterprise:** REST API
  `/orgs/{org}/copilot/usage` (daily aggregated usage metrics),
  `/enterprises/{enterprise}/copilot/usage`, Audit Log API
  `/orgs/{org}/audit-log` with action patterns
  `copilot.cfb_seat_*`/`copilot_user_*`, Compliance API for PR-
  related suggestions acceptance, GitHub webhook on
  `copilot_seat_management` events. Replicate logs to your own S3
  bucket via GitHub Actions or directly to SIEM (Splunk, Microsoft
  Sentinel, Elastic).

  **JetBrains AI Assistant:** audit events in JetBrains Toolbox
  Enterprise console; local logs in `~/Library/Logs/JetBrains/...`
  (macOS) or `%APPDATA%/JetBrains/...` (Windows) collected via MDM if
  needed.

  **Anthropic Claude Console (Workspaces):** Console → Settings →
  Activity Logs (90–180 days retention), audit log export via API,
  webhooks for workspace events; `claude code` CLI locally:
  `.claude/projects/<repo-hash>/*.jsonl` available locally, optionally
  centralizable.

  **OpenAI ChatGPT Enterprise/Team:** Compliance API for conversations,
  workspace activity logs, SIEM integration via API polling.

  **AWS Q Developer Pro:** AWS CloudTrail events, IAM Identity Center
  access logs, Q Developer Customizations audit.

  **Git Duo:** Git Audit Events stream (`Audit Streaming` feature
  with JSONL export to SIEM endpoint), project-level events.

  **Continue.dev/Tabby/Ollama locally:** `~/.continue/sessions/*.json`,
  Ollama logs in `journalctl -u ollama` or
  `~/Library/Logs/Ollama/server.log`.

  Retention period requirements: ≥ 1 year for standard audits, ≥ 6
  years for GDPR compliance ties, ≥ 10 years for CRA incident histories
  (Regulation (EU) 2024/2847 Art. 14). Storage location: central SIEM
  (Splunk Enterprise Security with `audit_logs` index, Microsoft
  Sentinel workspace with DCR rules, Elastic Security with dedicated
  data stream, IBM QRadar AQL query repository), append-only with
  WORM (S3 Object Lock compliance mode, Azure Immutable Blob Storage
  with time-based retention, Google Cloud Storage Bucket Lock). Access
  model: security team and compliance officer only (RBAC), four-eyes
  for deletions before retention expiry. Correlation: match AI tool
  logs with git commit logs (via `Co-developed-with` trailer from item
  10) and PR metadata to determine AI share per incident. Example
  SIEM query (Splunk):
  ```text
  index=audit_logs sourcetype=github_audit
  action=copilot_user.* user=<dev>
  | stats count by action _time
  ```
  Data protection compliance: logs typically contain usernames/email
  → enter in record of processing activities (GDPR Art. 30) as
  processing "audit logging AI tool usage", document technical and
  organizational measures per Art. 32, works council notification on
  co-determination right (BetrVG § 87(1) No. 6 in DE). References:
  ISO/IEC 27001 A.8.15 (logging) and A.5.34 (privacy and protection of
  PII), NIST SP 800-92 (Log Management), GDPR Art. 30, CRA Annex I
  Part II 2.1 lit. c (logging), BSI IT-Grundschutz OPS.1.1.5
  (Protokollierung), EU AI Act Art. 12 (record-keeping).
- **Akzeptanz / Acceptance:** Aufbewahrungsfrist und Pfad festgelegt
  (≥ 1 Jahr Standard, ≥ 6 Jahre DSGVO, ≥ 10 Jahre CRA);
  Werkzeug-spezifische Audit-Quellen in SIEM angebunden; WORM-Speicher
  aktiv; Verzeichnis von Verarbeitungstätigkeiten mit KI-Tool-Logging-
  Eintrag.
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

#### CL-09-14: Ausnahmen und Risikoakzeptanz / Exceptions and Risk Acceptance

- **DE:** Eine Abweichung von dieser Checkliste wird mit Begründung und
  Unterschrift festgehalten. Eine Risikoakzeptanz hat ein Ablaufdatum.
  Pflichtfelder je Risikoakzeptanz-Eintrag: (1) Eindeutige ID
  (`RA-AI-YYYY-NNN`), (2) betroffener Checklisten-Punkt (z. B. „Item 7
  Datenschutz: Prompt mit Kunden-ID an externe KI"), (3) Beschreibung
  der Abweichung (technisch und prozessual), (4) Begründung warum
  Standard-Anforderung nicht erfüllbar ist (zeitliche, technische,
  vertragliche Gründe), (5) Risikobewertung (CVSS-ähnliche Bewertung
  oder qualitative Skala niedrig/mittel/hoch/kritisch mit Begründung),
  (6) kompensierende Maßnahmen (zusätzliche Kontrollen, manuelle
  Überprüfung, eingeschränkter Anwendungsbereich), (7) Ablaufdatum
  (max. 6 Monate für mittlere Risiken, max. 3 Monate für hohe Risiken,
  max. 4 Wochen für kritische Risiken), (8) verantwortliche Person
  (Engineering Manager / Tech Lead), (9) Genehmiger (Security Lead/Security
  Lead bei mittlerem+, verantwortliche Leitung bei hohem+), (10)
  Genehmigungsdatum mit digitaler Signatur (S/MIME, GPG, oder
  qualifizierte elektronische Signatur via Adobe Sign/DocuSign mit
  eIDAS-Niveau). Vorlage in `docs/security/risk-register.md` mit
  Tabellenstruktur:
  ```text
  | ID         | Item    | Risiko | Ablauf     | Owner   | Status |
  |------------|---------|--------|------------|---------|--------|
  | RA-AI-2026-001 | Item 7 | mittel | 2026-10-01 | M.Mustermann | aktiv |
  ```
  Tooling: ServiceNow GRC, Archer IRM, OneTrust GRC, Diligent HighBond,
  einfacher Markdown-Tabellen-Eintrag mit signierten Commits. Vor
  Ablauf 30 Tage automatische Erinnerung an Owner; vor Ablauf 7 Tage
  Eskalation an Security Lead. Bei Ablauf ohne Verlängerung: KI-Tool-Zugriff
  oder betroffene Funktion automatisch sperren. Vier-Augen-Prinzip
  für Genehmigungen: Engineering Manager + Security Lead bei mittel+,
  Engineering Manager + Security Lead + Security Lead bei hoch+, zusätzlich
  verantwortliche Leitung/Vorstand bei kritisch. Quartalsbericht an
  verantwortliche Leitung mit allen aktiven Risikoakzeptanzen, Trend, Lessons
  Learned. Audit-Spur: alle Änderungen im Risikoregister im Git-Log
  oder GRC-Audit-Log nachvollziehbar; Zugriffsbeschränkung auf
  Security-Team und Compliance-Officer. Referenzen: ISO/IEC 27001
  A.5.7 (Threat intelligence), A.5.13 (Labelling of information),
  A.5.36 (Compliance), ISO/IEC 27005 (Information Security Risk
  Management), NIST SP 800-30 (Guide for Conducting Risk Assessments),
  NIST SP 800-37 (RMF), NIST AI RMF MANAGE-1.1 (risk acceptance), CRA
  Anhang VII (Konformitätsbewertung mit Risiko-Dokumentation),
  EU AI Act Art. 9 (Risk Management System).
- **EN:** A deviation from this checklist is recorded with
  justification and signature. A risk acceptance has an expiry date.
  Mandatory fields per risk acceptance entry: (1) unique ID
  (`RA-AI-YYYY-NNN`), (2) affected checklist item (e.g., "Item 7 data
  protection: prompt with customer ID to external AI"), (3) description
  of the deviation (technical and procedural), (4) justification why
  the standard requirement is not feasible (time, technical, contractual
  reasons), (5) risk assessment (CVSS-like rating or qualitative scale
  low/medium/high/critical with justification), (6) compensating
  controls (additional measures, manual review, restricted scope), (7)
  expiry date (max 6 months for medium risk, max 3 months for high
  risk, max 4 weeks for critical risk), (8) responsible person
  (Engineering Manager / Tech Lead), (9) approver (Security Lead/Security Lead
  for medium+, board for high+), (10) approval date with digital
  signature (S/MIME, GPG, or qualified electronic signature via Adobe
  Sign/DocuSign with eIDAS level). Template in
  `docs/security/risk-register.md` with table structure:
  ```text
  | ID         | Item    | Risk   | Expiry     | Owner   | Status |
  |------------|---------|--------|------------|---------|--------|
  | RA-AI-2026-001 | Item 7 | medium | 2026-10-01 | M.Mustermann | active |
  ```
  Tooling: ServiceNow GRC, Archer IRM, OneTrust GRC, Diligent
  HighBond, simple markdown table entry with signed commits. Automatic
  reminder to owner 30 days before expiry; escalation to Security Lead 7 days
  before expiry. On expiry without renewal: AI tool access or affected
  function blocked automatically. Four-eyes principle for approvals:
  Engineering Manager + Security Lead for medium+, Engineering Manager
  + Security Lead + Security Lead for high+, additionally executive board for
  critical. Quarterly report to executive board with all active risk
  acceptances, trend, lessons learned. Audit trail: all changes in the
  risk register traceable in git log or GRC audit log; access
  restriction to security team and compliance officer. References:
  ISO/IEC 27001 A.5.7 (threat intelligence), A.5.13 (labelling of
  information), A.5.36 (compliance), ISO/IEC 27005 (Information
  Security Risk Management), NIST SP 800-30 (Guide for Conducting Risk
  Assessments), NIST SP 800-37 (RMF), NIST AI RMF MANAGE-1.1 (risk
  acceptance), CRA Annex VII (conformity assessment with risk
  documentation), EU AI Act Art. 9 (Risk Management System).
- **Akzeptanz / Acceptance:** Eintrag im Sicherheits-/Risikoregister
  mit eindeutiger ID, betroffenem Item, Risikobewertung, Ablaufdatum,
  Owner, Genehmiger und digitaler Signatur; Vier-Augen-Genehmigung;
  automatische Erinnerung 30/7 Tage vor Ablauf; Quartalsbericht an
  verantwortliche Leitung.
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

#### CL-09-15: KI-Lieferkettentransparenz / AI Supply-Chain Transparency

- **DE:** Für jedes eingesetzte KI-Werkzeug, jeden KI-Dienst und jedes
  Modell werden die vom Anbieter verfügbaren Transparenzangaben im
  KI-Werkzeug-Inventar erfasst oder verlinkt: Modell-Identität und
  -Version, Verweis auf Model Card oder AI-SBOM des Anbieters, Trainings-
  und Feinabstimmungsverfahren sowie Herkunft und Sensitivität der
  Trainingsdaten soweit veröffentlicht, und KI-spezifische
  Sicherheitseigenschaften. Die Organisation erstellt für fremdbezogene Modelle
  keine eigene AI-SBOM; sie fordert die Angaben als Lieferketten-Nachweis
  an und dokumentiert, wo eine Angabe vom Anbieter nicht verfügbar ist.
  Bezugsrahmen ist die G7-Leitlinie „Software Bill of Materials for AI –
  Minimum Elements" (2026).
- **EN:** For every AI tool, AI service, and model in use, the
  transparency information available from the provider is recorded or
  linked in the AI tool inventory: model identity and version, a link to
  the provider's model card or AI-SBOM, training and fine-tuning method,
  the origin and sensitivity of the training data as far as published,
  and AI-specific security properties. Organisation does not produce its own
  AI-SBOM for externally sourced models; it requests the information as
  supply-chain evidence and documents where a provider does not make an
  item available. The reference framework is the G7 guideline "Software
  Bill of Materials for AI – Minimum Elements" (2026).
- **Akzeptanz / Acceptance:** Das KI-Werkzeug-Inventar nennt je Modell
  bzw. Dienst die Anbieter-Transparenzquelle (Model Card oder AI-SBOM)
  oder begründet deren Fehlen; offene Angaben sind als solche markiert. /
  The AI tool inventory names, per model or service, the provider
  transparency source (model card or AI-SBOM) or justifies its absence;
  open items are marked as such.
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

#### CL-09-16: KI-Regulierungs-Screening / AI Regulatory Screening

- **DE:** Bei KI-Werkzeugen, KI-Diensten, Modellen oder KI-Komponenten wird
  geprüft, ob neben dieser Checkliste auch regulatorische Anforderungen aus
  CL_Standards-Anwendbarkeit anwendbar sind. Das betrifft insbesondere EU
  AI Act, CRA, NIS2, DORA sowie Kunden- oder Sektorvorgaben. Reine Nutzung
  von KI als Entwicklungswerkzeug kann als `nicht anwendbar` dokumentiert
  werden, wenn kein KI-System, Modell oder Dienst Teil des ausgelieferten
  oder betriebenen Systems ist.
- **EN:** For AI tools, AI services, models, or AI components, check
  whether regulatory requirements from CL_Standards-Anwendbarkeit also
  apply. This covers in particular the EU AI Act, CRA, NIS2, DORA, and
  customer or sector rules. Pure use of AI as a development tool may be
  documented as `not applicable` when no AI system, model, or service is
  part of the released or operated system.
- **Akzeptanz / Acceptance:** KI-Inventar, Lieferkettennachweis oder
  Spec-Kit-Artefakt verlinkt das CL_01-Regulatory-Screening und nennt, ob
  die KI-Nutzung nur Werkzeugnutzung oder Bestandteil des Produkts bzw.
  Dienstes ist. / AI inventory, supply-chain evidence, or Spec Kit artefact
  links the CL_01 regulatory screening and states whether the AI use is
  tool-only use or part of the product or service.
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
- **Evidenz / Evidence:** _KI-Inventar, CL_01-Screening, Spec-Kit-Artefakt oder N/A-Begründung nennen. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name AI inventory, CL_01 screening, Spec Kit artefact, or N/A rationale. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-09-17: Didaktische Kommentare bei nichttrivialer Logik / Didactic Comments for Non-Trivial Logic

- **DE:** Bei KI-erzeugter oder KI-veränderter nichttrivialer Logik wird
  geprüft, ob kurze didaktische Kommentare den Review, die Wartung oder
  die Ausbildung unterstützen. Kommentare erklären das Warum, eine
  Randbedingung, eine Abwägung oder eine bekannte Grenze. Sie wiederholen
  nicht den offensichtlichen Codeablauf. Für lernende oder
  nutzerorientierte Artefakte bleiben Erklärungen Deutsch zuerst, Englisch
  danach und auf CEFR-B2-Niveau.
- **EN:** For AI-generated or AI-changed non-trivial logic, check whether
  short didactic comments support review, maintenance, or training.
  Comments explain the why, a boundary condition, a trade-off, or a known
  limit. They do not repeat the obvious code flow. For learner-facing or
  user-facing artefacts, explanations remain German first, English second,
  and at CEFR B2 level.
- **Akzeptanz / Acceptance:** Review oder PR zeigt, dass nichttriviale
  Logik auf sinnvolle Kommentare geprüft wurde; fehlende Kommentare sind
  begründet, übermäßige Kommentarflut wurde vermieden. / Review or PR
  shows that non-trivial logic was checked for useful comments; missing
  comments are justified, and excessive comment noise was avoided.
- **Referenz / Reference:** Spec-Kit `a11y-governance`,
  `didactic-code-comment-check-template`.
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
- **Evidenz / Evidence:** _PR, Review-Kommentar, Spec-Kit-Artefakt oder N/A-Begründung nennen. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name PR, review comment, Spec Kit artefact, or N/A rationale. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

### Akzeptanzkriterien gesamt / Overall Acceptance

**DE:** Erfüllt, wenn alle Punkte abgeschlossen sind und je Pull Request
mit nennenswertem KI-Anteil ein Reviewer-Vermerk vorliegt. KI-Regulatory-
Screening, KI-Lieferkettentransparenz und didaktische Kommentarprüfung sind
verlinkt oder begründet nicht anwendbar.

**EN:** Fulfilled when every item is closed and a reviewer note exists for
every pull request with a notable AI share. AI regulatory screening, AI
supply-chain transparency, and didactic comment review are linked or
justified as not applicable.

### Glossar / Glossary

**DE:** Dieses Glossar erklärt die wichtigsten Begriffe dieser Checkliste in Alltagssprache. Es ändert keine Anforderungen, sondern macht die vorhandenen Prüfpunkte leichter verständlich.

**EN:** This glossary explains the most important terms in this checklist in plain language. It does not change requirements; it makes the existing review items easier to understand.

<a id="cl-09-glossar-ai-code-assistenz"></a>

#### KI-Code-Assistenz / AI Coding Assistance

- **DE:** KI-Code-Assistenz unterstützt beim Schreiben, Erklären oder Ändern von Code. Die Vorschläge müssen geprüft werden, weil sie falsch oder unsicher sein können.
- **EN:** AI coding assistance helps write, explain, or change code. The suggestions must be reviewed because they can be wrong or insecure.

<a id="cl-09-glossar-llm"></a>

#### LLM / Large Language Model

- **DE:** Ein LLM ist ein großes Sprachmodell. Es erzeugt Text oder Code aus Prompts, versteht aber nicht automatisch Projektregeln oder Sicherheitsanforderungen.
- **EN:** An LLM is a large language model. It generates text or code from prompts but does not automatically understand project rules or security requirements.

<a id="cl-09-glossar-prompt"></a>

#### Prompt

- **DE:** Ein Prompt ist die Eingabe an ein KI-System. Er darf keine geheimen oder unzulässigen personenbezogenen Daten enthalten.
- **EN:** A prompt is the input to an AI system. It must not contain secrets or impermissible personal data.

<a id="cl-09-glossar-human-review"></a>

#### Menschliche Prüfung / Human Review

- **DE:** Menschliche Prüfung bedeutet, dass eine verantwortliche Person das Ergebnis bewertet. Bei KI-Ergebnissen ersetzt diese Prüfung kein Tool automatisch.
- **EN:** Human review means that a responsible person assesses the result. For AI outputs, this review is not automatically replaced by a tool.

<a id="cl-09-glossar-four-eyes"></a>

#### Vier-Augen-Prinzip / Four-Eyes Rule

- **DE:** Das Vier-Augen-Prinzip verlangt eine zweite qualifizierte Prüfung. Es ist besonders wichtig bei kritischer Logik, Sicherheit und Datenschutz.
- **EN:** The four-eyes rule requires a second qualified review. It is especially important for critical logic, security, and data protection.

<a id="cl-09-glossar-cve"></a>

#### CVE

- **DE:** Eine CVE ist eine weltweit eindeutige Kennung für eine bekannte Schwachstelle. Sie hilft, dieselbe Schwachstelle in Tools, Tickets und Meldungen eindeutig zu benennen.
- **EN:** A CVE is a globally unique identifier for a known vulnerability. It helps name the same vulnerability clearly in tools, tickets, and advisories.

<a id="cl-09-glossar-hallucinated-package"></a>

#### Halluziniertes Paket / Hallucinated Package

- **DE:** Ein halluziniertes Paket ist eine von KI vorgeschlagene Abhängigkeit, die nicht echt ist oder nicht zum Projekt passt. Vor Installation muss die Quelle geprüft werden.
- **EN:** A hallucinated package is an AI-suggested dependency that is not real or does not fit the project. The source must be checked before installation.

<a id="cl-09-glossar-dependency"></a>

#### Abhängigkeit / Dependency

- **DE:** Eine Abhängigkeit ist fremder Code oder ein Paket, das ein Projekt nutzt. Abhängigkeiten brauchen Pflege, Lizenzprüfung und Schwachstellenprüfung.
- **EN:** A dependency is third-party code or a package used by a project. Dependencies need maintenance, licence review, and vulnerability checks.

<a id="cl-09-glossar-personal-data"></a>

#### Personenbezogene Daten / Personal Data

- **DE:** Personenbezogene Daten sind Informationen, die sich auf eine identifizierte oder identifizierbare Person beziehen, zum Beispiel Name, Kennung, IP-Adresse oder Standortdaten.
- **EN:** Personal data is information relating to an identified or identifiable person, for example name, identifier, IP address, or location data.

<a id="cl-09-glossar-telemetry"></a>

#### Telemetrie / Telemetry

- **DE:** Telemetrie sind Nutzungs- oder Diagnosedaten, die ein Werkzeug sendet. Sie muss zu Datenschutz, Vertraulichkeit und Freigabe passen.
- **EN:** Telemetry is usage or diagnostic data sent by a tool. It must fit data protection, confidentiality, and approval rules.

<a id="cl-09-glossar-audit-trail"></a>

#### Audit-Spur / Audit Trail

- **DE:** Eine Audit-Spur zeigt nachvollziehbar, wer was wann getan oder entschieden hat. Sie hilft bei Prüfung, Fehlersuche und Verantwortung.
- **EN:** An audit trail traceably shows who did or decided what and when. It helps with review, troubleshooting, and accountability.

<a id="cl-09-glossar-ai-sbom-ml-bom"></a>

#### AI-SBOM / ML-BOM

- **DE:** Eine AI-SBOM oder ML-BOM beschreibt KI-Bestandteile, zum Beispiel Modelle, Datensätze, Frameworks und externe Dienste. Sie macht KI-Lieferketten nachvollziehbar.
- **EN:** An AI-SBOM or ML-BOM describes AI parts, for example models, datasets, frameworks, and external services. It makes AI supply chains traceable.

<a id="cl-09-glossar-eu-ai-act"></a>

#### EU AI Act

- **DE:** Der EU AI Act ist eine EU-Verordnung für KI-Systeme. Er kann Pflichten zu Risiko, Dokumentation, Transparenz und menschlicher Aufsicht auslösen.
- **EN:** The EU AI Act is an EU regulation for AI systems. It can trigger duties for risk, documentation, transparency, and human oversight.

<a id="cl-09-glossar-spec-kit"></a>

#### Spec Kit

- **DE:** Spec Kit ist ein werkzeuggestützter Ablauf für spezifikationsgetriebene Entwicklung. Es erzeugt und nutzt Markdown-Artefakte wie Spezifikation, Plan, Aufgaben und Analysen.
- **EN:** Spec Kit is a tool-supported flow for specification-driven development. It creates and uses Markdown artefacts such as specification, plan, tasks, and analyses.

<a id="cl-09-glossar-sdd"></a>

#### SDD / Specification-Driven Development

- **DE:** SDD bedeutet spezifikationsgetriebene Entwicklung. Erst werden Anforderungen, Plan und Prüfpunkte dokumentiert, danach wird umgesetzt.
- **EN:** SDD means specification-driven development. Requirements, plan, and review points are documented first; implementation follows afterwards.

<a id="cl-09-glossar-preset"></a>

#### Preset

- **DE:** Ein Preset ist eine vordefinierte Regel- oder Vorlagensammlung. Bei Spec Kit steuert es Governance, Prüfpunkte und erwartete Artefakte.
- **EN:** A preset is a predefined set of rules or templates. In Spec Kit it controls governance, review points, and expected artefacts.

<a id="cl-09-glossar-nachweis-matrix"></a>

#### Nachweis-Matrix / Evidence Matrix

- **DE:** Eine Nachweis-Matrix verbindet Prüfpunkte mit konkreten Belegen. Sie zeigt, welches Dokument, Ticket oder Artefakt welchen Prüfpunkt abdeckt.
- **EN:** An evidence matrix connects review points with concrete evidence. It shows which document, ticket, or artefact covers which review item.

### Versionshistorie / Version History

- **Version 1.0 (2026-04-27):** Erstfassung / Initial version
- **Version 1.1 (2026-04-27):** Erweiterte Durchführungshinweise, Quellen-URLs, Statusfelder und Beispiele / Extended guidance, source URLs, status fields, and examples
- **Version 1.2 (2026-04-30):** Barrierearme Personenbezeichnung ergänzt / Added accessible person wording
- **Version 1.3 (2026-05-19):** Prüfpunkt 15 „KI-Lieferkettentransparenz" ergänzt; Mitgeltende Dokumente um die G7-Leitlinie „Software Bill of Materials for AI – Minimum Elements" (2026) erweitert; synchron mit Richtlinie Sichere Entwicklung v2.4.0. / Added checklist item 15 "AI Supply-Chain Transparency"; extended related documents with the G7 guideline "Software Bill of Materials for AI – Minimum Elements" (2026); synchronized with Richtlinie Sichere Entwicklung v2.4.0.
- **Version 1.4 (2026-05-22):** Durchführungshinweise um Querverweis auf CL_12 und GitHub Spec Kit SDD für wesentliche Feature-Implementierungen mit agentischer KI ergänzt; synchron mit Richtlinie Sichere Entwicklung v2.5.0. / Added implementation guidance cross-reference to CL_12 and GitHub Spec Kit SDD for material feature implementations with agentic AI; synchronized with Richtlinie Sichere Entwicklung v2.5.0.
- **Version 1.5 (2026-06-12):** Durchführungshinweise präzisiert: Installation und Nachweis der Spec-Kit-Governance-Presets werden in CL_12 bewertet; synchron mit Richtlinie Sichere Entwicklung v2.6.0. / Refined implementation guidance: installation and evidence of the Spec Kit governance presets are assessed in CL_12; synchronized with Richtlinie Sichere Entwicklung v2.6.0.
- **Version 1.6 (2026-06-14):** Durchführungshinweise präzisiert: Aktualität und inhaltliche Abdeckung der Spec-Kit-Governance-Presets werden in CL_12 bewertet; synchron mit Richtlinie Sichere Entwicklung v2.7.0. / Refined implementation guidance: currency and content coverage of the Spec Kit governance presets are assessed in CL_12; synchronized with Richtlinie Sichere Entwicklung v2.7.0.
- **Version 1.7 (2026-06-14):** Durchführungshinweise präzisiert: Vollständige Spec-Kit-Artefakte mit Nachweis-Matrix können die separate manuelle Ausfüllung ersetzen; synchron mit Richtlinie Sichere Entwicklung v2.8.0. / Refined implementation guidance: complete Spec Kit artefacts with an evidence matrix may replace separate manual completion; synchronized with Richtlinie Sichere Entwicklung v2.8.0.
- **Version 1.8 (2026-06-15):** Prüfpunkte 16 und 17 zu KI-Regulierungs-Screening und didaktischen Kommentaren ergänzt; synchron mit Richtlinie Sichere Entwicklung v2.9.0. / Added items 16 and 17 for AI regulatory screening and didactic comments; synchronized with Richtlinie Sichere Entwicklung v2.9.0.

- **Version 1.9 (2026-06-16):** Verständlichkeit der Durchführungshinweise, Begründungs-, Evidenz- und Maßnahmenfelder für Entwickler:innen und Auszubildende präzisiert; CEFR-B2- und WCAG-2.2-AA-konforme Ausfüllhilfe ergänzt. / Refined understandability of implementation guidance, rationale, evidence, and action fields for developers and apprentices; added CEFR B2 and WCAG 2.2 AA conformant completion help.

- **Version 1.10 (2026-06-17):** Glossar und Begriff-Links für Entwickler:innen und Fachinformatik-Auszubildende ergänzt; wichtige Abkürzungen und Technologien in CEFR-B2-Sprache erklärt. / Added glossary and term links for developers and IT specialist apprentices; explained important abbreviations and technologies in CEFR B2 language.
- **Version 1.11 (2026-06-17):** Test-KPIs an Richtlinie Sichere Entwicklung v2.10.0 angepasst: KI-Code mindestens 80 % Line und Branch Coverage; Integrationstest-Abdeckung öffentlicher Schnittstellen und kritischer UI-Flows ergänzt. / Aligned test KPIs with Secure Development Guideline v2.10.0: AI code at least 80 % line and branch coverage; added integration-test coverage for public interfaces and critical UI flows.

---

- **Version 2.0.0 (2026-07-10):** Einheitliches zweiachsiges Statusmodell, stabile CL-IDs, Lernstufe, Rollen-, Evidenz-, Restrisiko- und Neubewertungsfelder sowie klare Trennung zwischen Vorlage und Projektnachweis eingeführt; synchron mit sichere-Entwicklung-Basis 3.0.0. / Added the unified two-axis status model, stable CL IDs, learning-stage, role, evidence, residual-risk, and re-evaluation fields, plus clear separation between template and project evidence; synchronized with secure-development baseline 3.0.0.
