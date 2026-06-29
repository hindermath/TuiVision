<!--
Quelle / Source: generische Ausbildungs- und Pruefgrundlage, bereinigt am 2026-06-17.
Dieses Dokument ist organisationsneutral und als generische Ausbildungs- und Pruefgrundlage formuliert.
Source: generic training and review baseline, generalized on 2026-06-17.
This document is organization-neutral and written as a generic training and review baseline.
-->

> **DE:** Diese Checkliste ist generisch und projektunabhaengig. Sie ist als Ausbildungs-, Review- und Haertungsgrundlage gedacht. Eine Nichtanwendbarkeit muss als `N/A` mit kurzer Begruendung dokumentiert werden.
>
> **EN:** This checklist is generic and project-independent. It is intended as a training, review, and hardening baseline. Non-applicability must be documented as `N/A` with a short rationale.

## Checkliste 02 – Sichere Softwarearchitektur / Secure Software Architecture

### Zweck / Purpose

**DE:** Diese Checkliste prüft, ob ein Software-Vorhaben die acht Prinzipien
sicherer Architektur einhält und ob die Architekturentscheidungen
nachvollziehbar dokumentiert sind.

**EN:** This checklist verifies that a software project follows the eight
secure architecture principles and that architectural decisions are
documented in a traceable way.

### Geltungsbereich / Scope

**DE:** Pflicht für jedes Projekt mit eigener Architektur (neu oder im Umbau).
Kein Pflichtdokument für reine Bugfix-Pakete ohne Architekturwirkung.

**EN:** Mandatory for every project with its own architecture (new or
refactor). Not required for pure bugfix releases without architectural
impact.

### Mitgeltende Dokumente / Related Documents

- Richtlinie Sichere Entwicklung
- ISO/IEC 27002:2022 A.8.27
- arc42-Vorlage Abschnitt 8 (Sicherheits-Querschnittskonzepte)
- Security Architecture Decision Records (S-ADR)
- BSI C3A (Criteria enabling Cloud Computing Autonomy)
- BSI C5 (Cloud Computing Compliance Criteria Catalogue)

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

**DE:** Bei einer REST-API ist die Browser/API-Grenze eine Vertrauensgrenze. Eingaben werden im Controller validiert und im Datenzugriff nochmals durch parametrisierte Queries geschuetzt. Das sind zwei Schichten und damit ein Defense-in-Depth-Beispiel.

**EN:** For a REST API, the browser/API boundary is a trust boundary. Inputs are validated in the controller and protected again in the data layer by parameterised queries. These are two layers and therefore an example of defense in depth.

### A11Y-Hinweise / A11Y Notes

**DE:** Beim Ausfüllen dieser Checkliste müssen alle Nachweise auch textlich verständlich sein. Verweise sollen beschreibende Linktexte haben. Screenshots, Diagramme oder Scan-Auszüge brauchen eine kurze Textbeschreibung. Der Status darf nicht nur über Farbe erkennbar sein.

**EN:** When this checklist is filled in, all evidence must also be understandable as text. References should use descriptive link text. Screenshots, diagrams, or scan extracts need a short text description. The status must not be shown by color alone.

### Wichtige Begriffe / Key Terms

**DE:** Die folgenden Begriffe kommen in dieser Checkliste vor. Die Links springen zum Glossar dieses Kapitels, damit Auszubildende und Entwickler:innen ohne Sicherheits-Spezialwissen die Begriffe direkt nachlesen können.

**EN:** The following terms appear in this checklist. The links jump to this chapter's glossary so that apprentices and developers without specialist security knowledge can look them up directly.

- [Vertrauensgrenze / Trust Boundary](#cl-02-glossar-trust-boundary)
- [Tiefenverteidigung / Defense in Depth](#cl-02-glossar-defense-in-depth)
- [Geringste Rechte / Least Privilege](#cl-02-glossar-least-privilege)
- [Angriffsfläche / Attack Surface](#cl-02-glossar-attack-surface)
- [API](#cl-02-glossar-api)
- [ADR / S-ADR](#cl-02-glossar-adr-sadr)
- [arc42](#cl-02-glossar-arc42)
- [BSI C3A](#cl-02-glossar-bsi-c3a)
- [BSI C5](#cl-02-glossar-bsi-c5)
- [Technische Dokumentation / Technical Documentation](#cl-02-glossar-technical-documentation)
- [Evidenz / Evidence](#cl-02-glossar-evidenz)

### Checkliste / Checklist

#### 1. Vertrauensgrenzen / Trust Boundaries

- **DE:** Vertrauensgrenzen (Trust Boundaries) trennen Bereiche mit
  unterschiedlichem Vertrauenslevel: Internet ↔ DMZ, DMZ ↔ Anwendungs-
  Backend, Anwendungs-Backend ↔ Datenbank, Container ↔ Host, Container A
  ↔ Container B, Browser ↔ Server. Jede Grenze ist im Architekturbild
  (z. B. C4-Modell, arc42-Bausteinsicht, Threat-Modeling-Diagramm in
  Microsoft Threat Modeling Tool oder pytm) markiert. An jeder Grenze
  gibt es einen Validierungspunkt: HTTP-Eingang validiert mit
  `jakarta.validation` (Bean Validation, Spring), Pydantic-Modellen
  (FastAPI), `FluentValidation` (.NET), `Joi`/`Zod` (Node.js); SQL-
  Eingaben werden parametrisiert (PreparedStatement, Entity Framework,
  SQLAlchemy); HTML-Output wird kontextspezifisch encoded
  (Thymeleaf-Auto-Escape, Razor-`@`-Operator, React-JSX-Default,
  DOMPurify für rich content). API-Schemas (OpenAPI/JSON Schema) sind
  als Single-Source-of-Truth im Repo, und Generatoren erzeugen Server-
  und Client-Validierung.
- **EN:** Trust boundaries separate zones with different trust levels:
  Internet ↔ DMZ, DMZ ↔ application backend, application backend ↔
  database, container ↔ host, container A ↔ container B, browser ↔
  server. Each boundary is marked in the architecture diagram (e.g.
  C4 model, arc42 building-block view, threat modeling diagram in
  Microsoft Threat Modeling Tool or pytm). At every boundary there is
  a validation point: HTTP input validated with `jakarta.validation`
  (Bean Validation, Spring), Pydantic models (FastAPI),
  `FluentValidation` (.NET), `Joi`/`Zod` (Node.js); SQL input is
  parameterised (PreparedStatement, Entity Framework, SQLAlchemy);
  HTML output is context-encoded (Thymeleaf auto-escape, Razor `@`
  operator, React JSX default, DOMPurify for rich content). API
  schemas (OpenAPI/JSON Schema) live as single source of truth in
  the repo, and generators produce server and client validation.
- **Akzeptanz / Acceptance:** Architekturbild zeigt alle Grenzen;
  Validierungs-Bibliothek je Grenze und Eingangsformat benannt;
  OpenAPI/JSON-Schema im Repo. / Architecture diagram shows all
  boundaries; validation library per boundary and input format
  named; OpenAPI/JSON Schema in the repo.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 2. Tiefenverteidigung / Defense in Depth

- **DE:** Defense in Depth bedeutet: ein Schutzmechanismus reicht nicht,
  weil jeder Mechanismus versagen kann. Beispiel-Schichten für ein
  Web-Backend: (1) Web Application Firewall oder Reverse Proxy mit
  Rate-Limiting (nginx, Cloudflare, AWS WAF, ModSecurity); (2)
  Eingangsvalidierung im Application-Code (Bean Validation, Pydantic,
  FluentValidation); (3) parametrisierte SQL-Queries; (4) Datenbank-
  Berechtigungen mit Read-Only-Rollen je nach Anwendungsfall; (5)
  Datenbank-Verschlüsselung at-rest (TDE in PostgreSQL/SQL Server,
  AWS RDS Encryption); (6) Audit-Logging mit unveränderlichem Speicher
  (Append-Only-Log, S3 Object Lock). Beispiel für ein API-Token:
  HTTPS + kurze TTL + Audience-Claim + Token-Revocation-Liste.
  Beispiel für Container: signiertes Image (Cosign) + Image-Scanner
  (Trivy, Grype) + Pod Security Standards + Network Policy +
  Runtime-Schutz (Falco, Cilium Tetragon). Schichten sollen aus
  unterschiedlichen Komponenten bestehen, sodass ein einzelner Bug
  nicht alle Schichten gleichzeitig kompromittiert.
- **EN:** Defense in depth means: a single mechanism is not enough,
  because every mechanism can fail. Example layers for a web backend:
  (1) web application firewall or reverse proxy with rate limiting
  (nginx, Cloudflare, AWS WAF, ModSecurity); (2) input validation in
  application code (Bean Validation, Pydantic, FluentValidation);
  (3) parameterised SQL queries; (4) database privileges with
  read-only roles per use case; (5) database encryption at rest
  (TDE in PostgreSQL/SQL Server, AWS RDS encryption); (6) audit
  logging with immutable storage (append-only log, S3 Object Lock).
  Example for an API token: HTTPS + short TTL + audience claim +
  token revocation list. Example for containers: signed image
  (Cosign) + image scanner (Trivy, Grype) + Pod Security Standards
  + network policy + runtime protection (Falco, Cilium Tetragon).
  Layers should be built from different components so a single bug
  cannot bypass them all at once.
- **Akzeptanz / Acceptance:** Tabelle mit kritischen Assets, je Asset
  mindestens zwei unabhängige Schutzschichten und zugehörige Tools. /
  Table of critical assets, with at least two independent protection
  layers and the matching tools per asset.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 3. Geringste Rechte / Least Privilege

- **DE:** Least Privilege heißt: jede Komponente und jedes Konto bekommt
  genau die Rechte, die für die Aufgabe nötig sind, nicht mehr.
  Beispiele: Container laufen als Nicht-Root-User
  (`USER 1000:1000` im Dockerfile, `runAsNonRoot: true` in der
  PodSecurity-Policy); Datenbank-Konten haben getrennte Rollen
  (`app_read`, `app_write`, `app_admin`); Kubernetes ServiceAccounts
  je Pod mit eigener RBAC-Rolle (`Role` und `RoleBinding`); AWS-IAM-
  Rollen je Service mit IAM-Policies, die nur benötigte Aktionen
  erlauben (kein `Action: "*"`); Linux-Capabilities reduziert
  (`securityContext.capabilities.drop: ["ALL"]`); Windows-Service-
  Konten nicht in `Administratoren`-Gruppe; CI-Tokens auf einzelne
  Repositories und einzelne Aktionen beschränkt
  (`permissions: contents: read` in GitHub Actions). Werkzeuge zur
  Prüfung: `kubescape`, `polaris`, `cloudsplaining` (AWS-IAM-
  Analyse), `iam-policy-validator` (AWS Access Analyzer).
  Berechtigungen werden mindestens einmal pro Quartal geprüft und
  bei Personal- oder Funktionswechsel sofort angepasst.
- **EN:** Least privilege means: every component and account gets
  exactly the rights needed for the task, no more. Examples:
  containers run as non-root user (`USER 1000:1000` in the
  Dockerfile, `runAsNonRoot: true` in the PodSecurity policy);
  database accounts use separate roles (`app_read`, `app_write`,
  `app_admin`); Kubernetes ServiceAccounts per pod with their own
  RBAC role (`Role` and `RoleBinding`); AWS IAM roles per service
  with IAM policies that allow only required actions (no
  `Action: "*"`); Linux capabilities dropped
  (`securityContext.capabilities.drop: ["ALL"]`); Windows service
  accounts not in `Administrators` group; CI tokens scoped to
  individual repositories and individual actions
  (`permissions: contents: read` in GitHub Actions). Verification
  tooling: `kubescape`, `polaris`, `cloudsplaining` (AWS IAM
  analysis), `iam-policy-validator` (AWS Access Analyzer).
  Permissions are reviewed at least once per quarter and adjusted
  immediately on staff or function changes.
- **Akzeptanz / Acceptance:** Rollen- und Rechte-Tabelle je Komponente
  mit Begründung; letzter Review-Zeitpunkt dokumentiert. / Role and
  permissions table per component with justification; last review
  date documented.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 4. Sichere Voreinstellungen / Fail-Safe Defaults

- **DE:** Fail-Safe Defaults heißt: ohne explizite Regel wird Zugriff
  abgelehnt; ein Fehler darf den geschützten Zustand niemals öffnen.
  Beispiele: Spring Security mit `http.authorizeHttpRequests().anyRequest().authenticated()`;
  ASP.NET Core mit `[Authorize]` als globale Default-Policy; Kubernetes
  NetworkPolicy mit Default-Deny (`policyTypes: Ingress`, leere
  `from:`-Liste); AWS Security Group ohne offene Inbound-Regeln; CSP-
  Header `default-src 'none'` und explizite Allow-Liste je
  Ressourcen-Typ; Feature-Flags „off by default", aktiviert nur
  bewusst pro Umgebung. Fehlerbehandlung: bei einer Exception wird
  die Sitzung beendet, kein Fallback auf anonyme Rolle, kein
  „silent allow" bei abgelaufenem Token. In Datenbanken: Spalten
  haben `NOT NULL` und sinnvolle Defaults, sodass eine
  unvollständige Eingabe nicht in einen halbgültigen Zustand führt.
  Anti-Pattern: `try { authorize() } catch { return ALLOWED; }`.
- **EN:** Fail-safe defaults means: without an explicit rule access is
  denied; an error must never open the protected state. Examples:
  Spring Security with
  `http.authorizeHttpRequests().anyRequest().authenticated()`; ASP.NET
  Core with `[Authorize]` as global default policy; Kubernetes
  NetworkPolicy with default-deny (`policyTypes: Ingress`, empty
  `from:` list); AWS Security Group with no open inbound rules; CSP
  header `default-src 'none'` plus explicit allow list per resource
  type; feature flags "off by default", enabled only deliberately
  per environment. Error handling: on an exception, the session
  ends, no fallback to an anonymous role, no "silent allow" on an
  expired token. In databases: columns are `NOT NULL` with sensible
  defaults so partial input cannot leave the system half-valid.
  Anti-pattern: `try { authorize() } catch { return ALLOWED; }`.
- **Akzeptanz / Acceptance:** Default-Deny-Konfigurationen für
  Authentifizierung, Netzwerk und CSP dokumentiert; Fehlerpfade
  prüfen den sicheren Endzustand. / Default-deny configuration for
  authentication, network, and CSP documented; error paths verify
  the safe end state.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 5. Angriffsfläche reduzieren / Reduce Attack Surface

- **DE:** In Produktion sind ungenutzte Endpunkte, Test-Routen, Debug-
  Endpunkte und Verwaltungs-UIs deaktiviert oder durch Authentifizierung
  geschützt. Konkrete Beispiele: Spring Boot Actuator-Endpunkte
  (`/actuator/heapdump`, `/actuator/env`) per
  `management.endpoints.web.exposure.include` einschränken; ASP.NET
  Core Swagger-UI nur in `Development` einbinden
  (`if (app.Environment.IsDevelopment()) { app.UseSwaggerUI(); }`);
  Django `DEBUG = False` in Produktion; Express `app.disable('x-powered-by')`;
  Container-Image ohne Build-Tools, Shells oder Paketmanager
  (`distroless`, `chainguard images`); Kubernetes ohne unnötige
  ServiceTypes (kein `LoadBalancer`, wenn Ingress reicht); SSH und
  RDP nur bei Bedarf, sonst über Bastion-Host oder ZTNA. Werkzeuge:
  `nmap` für Port-Scan; `nikto` für Web-Endpunkte; `kube-bench` für
  Kubernetes-CIS-Benchmark; SBOM-Diff zeigt überflüssige Pakete.
  Default-Passwörter und vorinstallierte Demo-Accounts sind vor
  Go-Live entfernt.
- **EN:** In production, unused endpoints, test routes, debug endpoints,
  and management UIs are disabled or protected by authentication.
  Concrete examples: Spring Boot Actuator endpoints
  (`/actuator/heapdump`, `/actuator/env`) restricted via
  `management.endpoints.web.exposure.include`; ASP.NET Core Swagger
  UI only in `Development`
  (`if (app.Environment.IsDevelopment()) { app.UseSwaggerUI(); }`);
  Django `DEBUG = False` in production; Express
  `app.disable('x-powered-by')`; container image without build tools,
  shells, or package managers (`distroless`, `chainguard images`);
  Kubernetes without unnecessary ServiceTypes (no `LoadBalancer`
  when Ingress suffices); SSH and RDP only when needed, otherwise
  via bastion or ZTNA. Tooling: `nmap` for port scans; `nikto` for
  web endpoints; `kube-bench` for the Kubernetes CIS benchmark;
  SBOM diff shows superfluous packages. Default passwords and
  preinstalled demo accounts are removed before go-live.
- **Akzeptanz / Acceptance:** Liste deaktivierter Funktionen und
  Endpunkte je Umgebung; Scan-Bericht ohne unbeabsichtigte
  Endpunkte. / List of disabled functions and endpoints per
  environment; scan report shows no unintended endpoints.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 6. Trennung von Belangen / Separation of Concerns

- **DE:** Querschnittsfunktionen werden zentral implementiert, nicht je
  Endpoint neu erfunden. Konkrete Bausteine: **Authentifizierung** in
  einem Filter/Middleware (`spring-security`-FilterChain, ASP.NET
  Core `app.UseAuthentication()`, FastAPI `Depends(get_current_user)`,
  Express `passport.authenticate(..)`); **Autorisierung** als
  zentrale Policy (Spring `@PreAuthorize`, ASP.NET Core `[Authorize]`-
  Attribute mit Policies, OPA/Rego als externer Policy-Service, Casbin);
  **Logging** über strukturierten Logger (SLF4J + Logback in Java,
  `Microsoft.Extensions.Logging` in .NET, `structlog` in Python,
  `pino` in Node.js) mit MDC/Correlation-IDs und ohne PII;
  **Eingabevalidierung** mit Bibliotheken (Bean Validation, Pydantic,
  FluentValidation, Joi/Zod) zentral je Endpoint-DTO; **Fehlerbehandlung**
  über globalen Handler (`@ControllerAdvice` in Spring,
  `app.UseExceptionHandler` in ASP.NET, `errorhandler` in Express);
  **Audit/Tracing** über OpenTelemetry mit gemeinsamem Service-Name.
  Anti-Pattern: in jeder Controller-Methode JWT manuell parsen oder
  `try/catch` mit individueller Antwort.
- **EN:** Cross-cutting concerns are implemented centrally, not
  reinvented per endpoint. Concrete building blocks:
  **Authentication** in a filter/middleware (`spring-security`
  filter chain, ASP.NET Core `app.UseAuthentication()`, FastAPI
  `Depends(get_current_user)`, Express `passport.authenticate(..)`);
  **Authorisation** as a central policy (Spring `@PreAuthorize`,
  ASP.NET Core `[Authorize]` attribute with policies, OPA/Rego as
  external policy service, Casbin); **Logging** via a structured
  logger (SLF4J + Logback in Java, `Microsoft.Extensions.Logging`
  in .NET, `structlog` in Python, `pino` in Node.js) with
  MDC/correlation IDs and without PII; **Input validation** using
  libraries (Bean Validation, Pydantic, FluentValidation, Joi/Zod)
  centrally per endpoint DTO; **Error handling** via a global
  handler (`@ControllerAdvice` in Spring,
  `app.UseExceptionHandler` in ASP.NET, `errorhandler` in
  Express); **Audit/tracing** via OpenTelemetry with a shared
  service name. Anti-pattern: parsing the JWT manually in every
  controller method or `try/catch` with bespoke responses.
- **Akzeptanz / Acceptance:** Pro Querschnittsfunktion ein zentrales
  Modul/Bibliothek benannt; je Funktion ein Verweis auf die
  Implementierung im Repo. / Per cross-cutting concern a central
  module/library named; per concern a reference to the
  implementation in the repo.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 7. Sichere Konfiguration / Secure Configuration

- **DE:** Geheimnisse (Passwörter, API-Keys, Tokens, Verschlüsselungs-
  Schlüssel, Datenbank-Credentials) liegen in geeigneten Secret-Stores
  und nie im Quellcode oder in `.env`-Dateien im Git-Repo. Konkrete
  Stores: HashiCorp Vault, AWS Secrets Manager, Azure Key Vault,
  Google Secret Manager, Kubernetes External Secrets Operator,
  Doppler, 1Password Connect. Konfiguration nicht-geheimer Werte:
  in YAML/JSON mit Umgebungs-Overlay (Spring `application-prod.yaml`,
  ASP.NET `appsettings.Production.json`, Helm-`values-prod.yaml`).
  Pre-Commit-Schutz: `gitleaks`, `trufflehog`, `detect-secrets`,
  GitHub-Native-Secret-Scanning. Bei einem Leak: sofort rotieren,
  Audit-Trail prüfen, Vorfall im Sicherheits-Logbuch eintragen,
  Push-Protection für die Zukunft aktivieren. Container-Secrets
  werden über Volume-Mount oder Secrets-Manager-Sidecar (Vault Agent
  Injector, External Secrets Operator) eingebracht, nicht über
  Umgebungsvariablen, die in Process-Listings sichtbar sind.
- **EN:** Secrets (passwords, API keys, tokens, encryption keys,
  database credentials) live in suitable secret stores and never in
  source code or `.env` files inside the Git repo. Concrete stores:
  HashiCorp Vault, AWS Secrets Manager, Azure Key Vault, Google
  Secret Manager, Kubernetes External Secrets Operator, Doppler,
  1Password Connect. Configuration of non-secret values: in YAML/
  JSON with environment overlays (Spring `application-prod.yaml`,
  ASP.NET `appsettings.Production.json`, Helm `values-prod.yaml`).
  Pre-commit protection: `gitleaks`, `trufflehog`, `detect-secrets`,
  GitHub native secret scanning. On a leak: rotate immediately,
  review audit trail, record the incident in the security log,
  enable push protection going forward. Container secrets are
  injected via volume mount or secrets-manager sidecar (Vault Agent
  Injector, External Secrets Operator), not via environment
  variables that appear in process listings.
- **Akzeptanz / Acceptance:** Secret-Store-Pfad, Inject-Mechanismus
  und Pre-Commit-Scanner-Konfiguration je Repository
  dokumentiert. / Secret store path, injection mechanism, and
  pre-commit scanner configuration documented per repository.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 8. Lieferketten-Sicherheit / Supply-Chain Security

- **DE:** Abhängigkeiten kommen aus geprüften, vertrauenswürdigen Registries:
  Maven Central, npm Registry mit `npm audit signatures`, NuGet.org,
  PyPI mit Sigstore, Docker Hub Verified Publishers, GitHub Container
  Registry, eigener Artifactory/Nexus-Proxy. Lock-Dateien sind
  eingecheckt: `package-lock.json` (npm), `yarn.lock` (Yarn),
  `pnpm-lock.yaml` (pnpm), `poetry.lock` / `requirements.txt` mit
  Hash (Python), `Pipfile.lock` (Pipenv), `go.sum` (Go),
  `Cargo.lock` (Rust), `Gemfile.lock` (Ruby), `packages.lock.json`
  (NuGet). CVE-Scanning: GitHub Dependabot, Snyk, Trivy,
  OWASP Dependency-Check, `pip-audit`, `npm audit`,
  `dotnet list package --vulnerable`, `cargo audit`. Container-Image-
  Scanning mit Trivy oder Grype gegen die SBOM. Verwundbare
  Abhängigkeiten mit CVSS ≥ 7.0 müssen vor Release gepatcht oder mit
  VEX als nicht-ausnutzbar dokumentiert werden. Pinning per Hash
  (z. B. `pip install --require-hashes`, `npm install --package-lock-only`)
  schützt gegen Repository-Manipulation; Renovate oder Dependabot
  hält Abhängigkeiten aktiv und automatisiert PRs.
- **EN:** Dependencies come from vetted, trusted registries: Maven
  Central, npm Registry with `npm audit signatures`, NuGet.org, PyPI
  with Sigstore, Docker Hub Verified Publishers, GitHub Container
  Registry, own Artifactory/Nexus proxy. Lock files are committed:
  `package-lock.json` (npm), `yarn.lock` (Yarn), `pnpm-lock.yaml`
  (pnpm), `poetry.lock` / `requirements.txt` with hashes (Python),
  `Pipfile.lock` (Pipenv), `go.sum` (Go), `Cargo.lock` (Rust),
  `Gemfile.lock` (Ruby), `packages.lock.json` (NuGet). CVE
  scanning: GitHub Dependabot, Snyk, Trivy, OWASP Dependency-Check,
  `pip-audit`, `npm audit`, `dotnet list package --vulnerable`,
  `cargo audit`. Container image scanning with Trivy or Grype
  against the SBOM. Vulnerable dependencies with CVSS ≥ 7.0 must
  be patched before release or documented as non-exploitable via
  VEX. Pinning by hash (e.g. `pip install --require-hashes`,
  `npm install --package-lock-only`) protects against registry
  manipulation; Renovate or Dependabot keeps dependencies active
  and automates PRs.
- **Akzeptanz / Acceptance:** Registry-Liste, Lock-Datei und aktueller
  CVE-Scan-Bericht im Repository; Update-Bot konfiguriert. /
  Registry list, lock file, and current CVE scan report in the
  repository; update bot configured.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 9. Sprachspezifische Architekturhinweise / Language-Specific Notes

- **DE:** Pro genutzter Sprache und Framework werden die etablierten
  Sicherheitsbibliotheken eingesetzt: **Java/Spring**: Spring Security
  für Auth, Spring Validation für Input, Hibernate Validator,
  BouncyCastle für Krypto-Erweiterungen, OWASP Java HTML Sanitizer
  gegen XSS; **Java/Jakarta EE**: Jakarta Security, Jakarta Bean
  Validation; **.NET**: ASP.NET Core Identity (oder Duende
  IdentityServer), `[Authorize]`-Attribute mit Policies, Antiforgery-
  Tokens, `Microsoft.AspNetCore.DataProtection`, FluentValidation;
  **Python/Django**: `django.contrib.auth`, CSRF-Middleware, Django
  ORM-Parameter-Binding, `django-axes` gegen Brute-Force; **Python/
  FastAPI**: Pydantic, OAuth2-Flows aus FastAPI-Security, Dependency-
  Injection für Auth; **Python/Flask**: Flask-Login, Flask-WTF mit
  CSRF, SQLAlchemy mit Parameter-Binding; **Node.js/Express**:
  `helmet` für Security-Header, `express-rate-limit`, `passport.js`
  für Auth, Joi/Zod für Validierung; **Go**: `crypto/*`, `net/http`
  mit `httprouter`/Chi und Middleware-Chain, `sqlx`/`pgx` für
  parametrisierte Queries; **Rust**: `ring`/`rustls`, `serde` mit
  Validatoren, `actix-web`/`axum` mit Tower-Middleware; **TypeScript/
  NestJS**: `@nestjs/passport`, Class-Validator, Helmet-Modul.
  Eigenimplementierungen von Auth, Krypto oder CSRF-Schutz sind
  verboten.
- **EN:** For each language and framework in use, established security
  libraries are applied: **Java/Spring**: Spring Security for auth,
  Spring Validation for input, Hibernate Validator, BouncyCastle for
  crypto extensions, OWASP Java HTML Sanitizer against XSS;
  **Java/Jakarta EE**: Jakarta Security, Jakarta Bean Validation;
  **.NET**: ASP.NET Core Identity (or Duende IdentityServer),
  `[Authorize]` attribute with policies, antiforgery tokens,
  `Microsoft.AspNetCore.DataProtection`, FluentValidation;
  **Python/Django**: `django.contrib.auth`, CSRF middleware, Django
  ORM parameter binding, `django-axes` against brute force;
  **Python/FastAPI**: Pydantic, OAuth2 flows from FastAPI Security,
  dependency injection for auth; **Python/Flask**: Flask-Login,
  Flask-WTF with CSRF, SQLAlchemy with parameter binding;
  **Node.js/Express**: `helmet` for security headers,
  `express-rate-limit`, `passport.js` for auth, Joi/Zod for
  validation; **Go**: `crypto/*`, `net/http` with `httprouter`/Chi
  and a middleware chain, `sqlx`/`pgx` for parameterised queries;
  **Rust**: `ring`/`rustls`, `serde` with validators, `actix-web`/
  `axum` with Tower middleware; **TypeScript/NestJS**:
  `@nestjs/passport`, class-validator, Helmet module. Custom
  implementations of auth, crypto, or CSRF protection are
  forbidden.
- **Akzeptanz / Acceptance:** Pro Sprache und Framework drei bis fünf
  Muster mit Bibliothek im Architekturdokument benannt. / Per
  language and framework, three to five patterns with library named
  in the architecture document.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 10. S-ADR-Pflicht / S-ADR Obligation

- **DE:** Sicherheits-ADRs (Security Architecture Decision Records) sind
  kurze Markdown-Dateien mit fester Struktur: **Status** (Proposed,
  Accepted, Deprecated, Superseded by), **Kontext**, **Entscheidung**,
  **Optionen** mit Vor- und Nachteilen, **Folgen** und **Compliance-
  Bezug** (z. B. „erfüllt ASVS V2.1.1, V6.2.3" oder „adressiert
  CAPEC-66"). Werkzeuge: `adr-tools` von Nat Pryce
  (`adr new "Choose JWT vs Session"`); MADR-Template aus
  `https://github.com/adr/madr`; Log4brains für Web-Visualisierung.
  Pflichtfälle für ein S-ADR: Wechsel des Authentifizierungs-Modells
  (Session ↔ JWT ↔ OAuth2), Wahl des Verschlüsselungs-Schlüssel-
  Stores, Einführung eines neuen Trust Boundary, Wechsel der TLS-
  Bibliothek, Entscheidung gegen einen empfohlenen Standard mit
  Risikobewertung. S-ADRs liegen versioniert in
  `docs/security/adr/0001-titel.md` und werden in PRs mit verlinkt.
  Rückwärtsbezug: spätere ADRs verweisen mit „Supersedes ADR-0007"
  auf abgelöste Entscheidungen.
- **EN:** Security ADRs (Security Architecture Decision Records) are
  short Markdown files with a fixed structure: **Status** (Proposed,
  Accepted, Deprecated, Superseded by), **Context**, **Decision**,
  **Options** with pros and cons, **Consequences**, and
  **Compliance reference** (e.g. "satisfies ASVS V2.1.1, V6.2.3" or
  "addresses CAPEC-66"). Tooling: `adr-tools` by Nat Pryce
  (`adr new "Choose JWT vs Session"`); MADR template from
  `https://github.com/adr/madr`; Log4brains for web visualisation.
  Mandatory cases for an S-ADR: change of authentication model
  (session ↔ JWT ↔ OAuth2), choice of encryption key store,
  introduction of a new trust boundary, change of TLS library,
  decision to deviate from a recommended standard with a risk
  assessment. S-ADRs live versioned in
  `docs/security/adr/0001-title.md` and are linked from PRs.
  Back-reference: later ADRs use "Supersedes ADR-0007" to point to
  retired decisions.
- **Akzeptanz / Acceptance:** S-ADR-Verzeichnis vorhanden mit
  mindestens den Pflichtfällen aus dem Projekt; jedes S-ADR enthält
  Compliance-Bezug. / S-ADR directory present with at least the
  mandatory cases for the project; every S-ADR contains a
  compliance reference.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 11. arc42 Abschnitt 8 / arc42 Section 8

- **DE:** arc42 ist ein freies Architektur-Template (12 Abschnitte) von
  Gernot Starke und Peter Hruschka. Abschnitt 8 „Querschnittliche
  Konzepte" beschreibt wiederverwendete Konzepte. Der Sicherheits-
  Anteil je Thema sollte konkrete Antworten geben:
  **Authentifizierung** — gewähltes Verfahren (OAuth2/OIDC, SAML,
  Session+Cookie, mTLS), Identity-Provider, MFA-Pflicht, Token-TTL;
  **Autorisierung** — Modell (RBAC, ABAC, ReBAC), Bibliothek (Spring
  Security, ASP.NET Authorization, OPA, Casbin), Default-Deny;
  **Verschlüsselung** — Bibliothek je Sprache, AES-Modus, TLS-Version,
  Schlüssel-Store, Schlüssel-Rotation; **Eingabevalidierung** —
  zentrales Validierungsmodul, Schema-Quelle (OpenAPI/JSON Schema);
  **Fehlerbehandlung** — globaler Handler, kein Stack-Trace im API-
  Output, einheitliches Error-Response-Schema (`application/problem+json`,
  RFC 7807); **Logging** — Log-Format (JSON, Logfmt), Levels,
  Sensitivitätsfilter, Aufbewahrungsfrist, zentrale Plattform (ELK,
  Loki/Grafana, Splunk, Datadog); **Abhängigkeiten** — Registry,
  Lock-Files, CVE-Scanner, Update-Bot; **Deployment** — Pipeline,
  Image-Signatur, Provenance, Rollback-Plan. Werkzeuge:
  arc42-Template (Markdown, AsciiDoc) von `https://arc42.org/`,
  Structurizr DSL für C4-Diagramme, dokumentationsgetriebene
  Architektur mit Diátaxis-Struktur.
- **EN:** arc42 is a free architecture template (12 sections) by
  Gernot Starke and Peter Hruschka. Section 8 "Cross-cutting
  Concepts" describes reusable concepts. The security portion per
  topic should give concrete answers: **Authentication** — chosen
  scheme (OAuth2/OIDC, SAML, session+cookie, mTLS), identity
  provider, MFA requirement, token TTL; **Authorization** — model
  (RBAC, ABAC, ReBAC), library (Spring Security, ASP.NET
  Authorization, OPA, Casbin), default-deny; **Encryption** —
  library per language, AES mode, TLS version, key store, key
  rotation; **Input validation** — central validation module,
  schema source (OpenAPI/JSON Schema); **Error handling** — global
  handler, no stack trace in API output, unified error response
  schema (`application/problem+json`, RFC 7807); **Logging** — log
  format (JSON, logfmt), levels, sensitivity filter, retention,
  central platform (ELK, Loki/Grafana, Splunk, Datadog);
  **Dependencies** — registry, lock files, CVE scanner, update bot;
  **Deployment** — pipeline, image signature, provenance, rollback
  plan. Tooling: arc42 template (Markdown, AsciiDoc) from
  `https://arc42.org/`, Structurizr DSL for C4 diagrams,
  documentation-driven architecture with the Diátaxis structure.
- **Akzeptanz / Acceptance:** arc42 §8 mit allen acht
  Sicherheitsthemen oder N/A mit Begründung; Diagramm-Referenzen
  und Tool-Namen je Thema. / arc42 §8 with all eight security
  topics or N/A with justification; diagram references and tool
  names per topic.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 12. Cloud-Autonomie und digitale Souveränität / Cloud Autonomy and Digital Sovereignty

- **DE:** Wenn ein Projekt Cloud-Dienste, SaaS, PaaS, IaaS, verwaltete
  Dienste, Container-Registries, Artefakt-Hosting oder providergebundene
  Deployments wesentlich nutzt oder bereitstellt, wird BSI C3A als
  Bewertungsrahmen für selbstbestimmte Cloud-Nutzung geprüft. Die
  Architektur dokumentiert Provider-Abhängigkeiten, Lock-in-Risiken,
  Exit- oder Portabilitätsoptionen, Daten- und Betriebsstandorte sowie
  betroffene S-ADRs. Reine Entwicklungsinfrastruktur darf mit kurzer
  Toolchain-Begründung als `nicht anwendbar` dokumentiert werden.
- **EN:** If a project materially uses or provides cloud services, SaaS,
  PaaS, IaaS, managed services, container registries, artefact hosting, or
  provider-dependent deployments, BSI C3A is checked as an assessment
  framework for self-determined cloud use. The architecture documents
  provider dependencies, lock-in risks, exit or portability options, data
  and operation locations, and affected S-ADRs. Generic development
  infrastructure may be documented as `not applicable` with a short
  toolchain rationale.
- **Akzeptanz / Acceptance:** `docs/security/cloud-autonomy-applicability.md`
  oder ein gleichwertiges Spec-Kit-Artefakt nennt Entscheidung, Cloud-
  Service-Scope, Provider-Abhängigkeiten, Lock-in-/Portabilitätsrisiken,
  Exit-Optionen, S-ADR-Links und N/A-Begründung. / `docs/security/cloud-
  autonomy-applicability.md` or an equivalent Spec Kit artefact states the
  decision, cloud-service scope, provider dependencies, lock-in and
  portability risks, exit options, S-ADR links, and N/A rationale.
- **Referenz / Reference:** Spec-Kit `architecture-governance`, Template
  `cloud-autonomy-applicability-template`.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Cloud-Autonomie-Nachweis, S-ADR, Architekturentscheidung oder N/A-Begründung nennen. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name cloud autonomy evidence, S-ADR, architecture decision, or N/A rationale. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 13. Cloud-Compliance-Assurance / Cloud Compliance Assurance

- **DE:** Wenn ein Projekt Cloud-Dienste wesentlich nutzt oder bereitstellt,
  wird BSI C5 als Assurance-Rahmen geprüft. Die Architektur dokumentiert,
  ob ein C5-Testat oder ein gleichwertiger Nachweis vorliegt, welchen
  Umfang dieser Nachweis abdeckt, welche Shared-Responsibility-Lücken
  bleiben, welche Subdienstleister betroffen sind und welche
  Kundenpflichten offen sind. Fehlt ein C5-Nachweis, werden Ersatznachweise
  und Restrisiken dokumentiert.
- **EN:** If a project materially uses or provides cloud services, BSI C5
  is checked as an assurance framework. The architecture documents whether
  a C5 attestation or equivalent evidence exists, which scope it covers,
  which shared-responsibility gaps remain, which subprocessors are
  involved, and which customer-side duties remain open. If C5 evidence is
  missing, substitute evidence and residual risks are documented.
- **Akzeptanz / Acceptance:** `docs/security/cloud-compliance-assurance.md`
  oder ein gleichwertiges Spec-Kit-Artefakt nennt C5-/Assurance-Status,
  Scope, Provider/Subdienstleister, Shared-Responsibility-Gaps,
  Datenstandort, Logging-/Backup-Nachweise, S-ADR-Links und offene Risiken.
  / `docs/security/cloud-compliance-assurance.md` or an equivalent Spec Kit
  artefact states C5 or assurance status, scope, provider/subprocessors,
  shared-responsibility gaps, data location, logging and backup evidence,
  S-ADR links, and open risks.
- **Referenz / Reference:** Spec-Kit `architecture-governance`, Template
  `cloud-compliance-assurance-template`.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _C5-Testat, Assurance-Bericht, Architekturentscheidung, Risikoeintrag oder N/A-Begründung nennen. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name C5 attestation, assurance report, architecture decision, risk entry, or N/A rationale. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

### Akzeptanzkriterien gesamt / Overall Acceptance

**DE:** Erfüllt, wenn alle Punkte abgeschlossen sind und die Evidenz unter
`docs/security/` und `docs/security/adr/` auffindbar ist. Bei wesentlicher
Cloud-Nutzung sind C3A- und C5-Entscheidungen dokumentiert oder begründet
als nicht anwendbar markiert.

**EN:** Fulfilled when every item is closed and the evidence under
`docs/security/` and `docs/security/adr/` is findable. For material cloud
use, C3A and C5 decisions are documented or marked as not applicable with
rationale.

### Glossar / Glossary

**DE:** Dieses Glossar erklärt die wichtigsten Begriffe dieser Checkliste in Alltagssprache. Es ändert keine Anforderungen, sondern macht die vorhandenen Prüfpunkte leichter verständlich.

**EN:** This glossary explains the most important terms in this checklist in plain language. It does not change requirements; it makes the existing review items easier to understand.

<a id="cl-02-glossar-trust-boundary"></a>

#### Vertrauensgrenze / Trust Boundary

- **DE:** Eine Vertrauensgrenze trennt Bereiche mit unterschiedlichem Schutzbedarf oder unterschiedlicher Kontrolle. Daten, die diese Grenze überschreiten, brauchen besondere Prüfung.
- **EN:** A trust boundary separates areas with different protection needs or different control. Data crossing this boundary needs special review.

<a id="cl-02-glossar-defense-in-depth"></a>

#### Tiefenverteidigung / Defense in Depth

- **DE:** Tiefenverteidigung bedeutet mehrere Schutzschichten. Wenn eine Maßnahme ausfällt, sollen weitere Maßnahmen den Schaden begrenzen.
- **EN:** Defense in depth means several layers of protection. If one measure fails, other measures should limit the damage.

<a id="cl-02-glossar-least-privilege"></a>

#### Geringste Rechte / Least Privilege

- **DE:** Geringste Rechte bedeutet, dass Personen, Dienste und Programme nur die Berechtigungen bekommen, die sie für ihre Aufgabe wirklich brauchen.
- **EN:** Least privilege means that people, services, and programs get only the permissions they really need for their task.

<a id="cl-02-glossar-attack-surface"></a>

#### Angriffsfläche / Attack Surface

- **DE:** Die Angriffsfläche umfasst alle Stellen, über die ein System angegriffen werden kann, zum Beispiel APIs, Login-Seiten, Ports, Dateien oder Abhängigkeiten.
- **EN:** The attack surface includes all places where a system can be attacked, for example APIs, login pages, ports, files, or dependencies.

<a id="cl-02-glossar-api"></a>

#### API

- **DE:** Eine API ist eine Schnittstelle, über die Programme miteinander sprechen. APIs brauchen klare Regeln für Authentifizierung, Eingaben, Fehler und Berechtigungen.
- **EN:** An API is an interface through which programs communicate. APIs need clear rules for authentication, inputs, errors, and permissions.

<a id="cl-02-glossar-adr-sadr"></a>

#### ADR / S-ADR

- **DE:** Ein ADR dokumentiert eine Architekturentscheidung. Ein S-ADR ist ein ADR mit Sicherheitsbezug, zum Beispiel zu Authentifizierung, Kryptografie oder Netzwerkgrenzen.
- **EN:** An ADR documents an architecture decision. An S-ADR is an ADR with security relevance, for example authentication, cryptography, or network boundaries.

<a id="cl-02-glossar-arc42"></a>

#### arc42

- **DE:** arc42 ist eine Vorlage für Architekturdokumentation. Sie hilft, Struktur, Schnittstellen, Risiken und Entscheidungen eines Systems nachvollziehbar zu beschreiben.
- **EN:** arc42 is a template for architecture documentation. It helps describe a system’s structure, interfaces, risks, and decisions in a traceable way.

<a id="cl-02-glossar-bsi-c3a"></a>

#### BSI C3A

- **DE:** BSI C3A ist eine BSI-Hilfestellung für Cloud-Anforderungen. In dieser Checkliste wird sie genutzt, um Cloud-Autonomie und digitale Souveränität zu prüfen.
- **EN:** BSI C3A is BSI guidance for cloud requirements. In this checklist it is used to review cloud autonomy and digital sovereignty.

<a id="cl-02-glossar-bsi-c5"></a>

#### BSI C5

- **DE:** BSI C5 ist ein Kriterienkatalog für Cloud-Sicherheit. Er hilft, Cloud-Anbieter und Cloud-Dienste anhand nachvollziehbarer Kontrollen zu bewerten.
- **EN:** BSI C5 is a criteria catalogue for cloud security. It helps assess cloud providers and services using traceable controls.

<a id="cl-02-glossar-technical-documentation"></a>

#### Technische Dokumentation / Technical Documentation

- **DE:** Technische Dokumentation beschreibt Produkt, Architektur, Risiken, Sicherheitsmaßnahmen, Tests und Nachweise so, dass Prüfung und Wartung möglich sind.
- **EN:** Technical documentation describes product, architecture, risks, security measures, tests, and evidence so that review and maintenance are possible.

<a id="cl-02-glossar-evidenz"></a>

#### Evidenz / Evidence

- **DE:** Evidenz ist ein prüfbarer Nachweis. Das kann ein Link, Ticket, Scan-Bericht, Pull Request, Protokoll, Architekturdiagramm oder Dokument sein.
- **EN:** Evidence is verifiable proof. It can be a link, ticket, scan report, pull request, record, architecture diagram, or document.

### Versionshistorie / Version History

- **Version 1.0 (2026-04-27):** Erstfassung / Initial version
- **Version 1.1 (2026-04-27):** Erweiterte Durchführungshinweise, Quellen-URLs, Statusfelder und Beispiele / Extended guidance, source URLs, status fields, and examples
- **Version 1.2 (2026-06-15):** Prüfpunkte 12 und 13 zu BSI C3A Cloud-Autonomie und BSI C5 Cloud-Compliance-Assurance ergänzt; synchron mit Richtlinie Sichere Entwicklung v2.9.0. / Added items 12 and 13 for BSI C3A cloud autonomy and BSI C5 cloud compliance assurance; synchronized with Richtlinie Sichere Entwicklung v2.9.0.

- **Version 1.3 (2026-06-16):** Verständlichkeit der Durchführungshinweise, Begründungs-, Evidenz- und Maßnahmenfelder für Entwickler:innen und Auszubildende präzisiert; CEFR-B2- und WCAG-2.2-AA-konforme Ausfüllhilfe ergänzt. / Refined understandability of implementation guidance, rationale, evidence, and action fields for developers and apprentices; added CEFR B2 and WCAG 2.2 AA conformant completion help.

- **Version 1.4 (2026-06-17):** Glossar und Begriff-Links für Entwickler:innen und Fachinformatik-Auszubildende ergänzt; wichtige Abkürzungen und Technologien in CEFR-B2-Sprache erklärt. / Added glossary and term links for developers and IT specialist apprentices; explained important abbreviations and technologies in CEFR B2 language.

---
