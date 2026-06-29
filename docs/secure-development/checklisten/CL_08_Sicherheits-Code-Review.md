<!--
Quelle / Source: generische Ausbildungs- und Pruefgrundlage, bereinigt am 2026-06-17.
Dieses Dokument ist organisationsneutral und als generische Ausbildungs- und Pruefgrundlage formuliert.
Source: generic training and review baseline, generalized on 2026-06-17.
This document is organization-neutral and written as a generic training and review baseline.
-->

> **DE:** Diese Checkliste ist generisch und projektunabhaengig. Sie ist als Ausbildungs-, Review- und Haertungsgrundlage gedacht. Eine Nichtanwendbarkeit muss als `N/A` mit kurzer Begruendung dokumentiert werden.
>
> **EN:** This checklist is generic and project-independent. It is intended as a training, review, and hardening baseline. Non-applicability must be documented as `N/A` with a short rationale.

## Checkliste 08 – Sicherheits-Code-Review / Security Code Review

### Zweck / Purpose

**DE:** Diese Checkliste leitet Reviewer durch die wichtigsten Sicherheitsthemen
beim Code-Review. Sie deckt sprachunabhängige und sprachspezifische Punkte ab.

**EN:** This checklist guides reviewers through the key security topics in
a code review. It covers both language-independent and language-specific
items.

### Geltungsbereich / Scope

**DE:** Anzuwenden auf jeden Pull Request, der Produktivcode oder Konfigurationen
ändert. Bei reinen Dokumentationsänderungen entfällt die Checkliste.

**EN:** Applies to every pull request that changes production code or
configuration. Pure documentation changes are exempt.

### Mitgeltende Dokumente / Related Documents

- Richtlinie Sichere Entwicklung
- Leitlinie für sichere Programmierung (mit Anhängen je Sprache)
- ISO/IEC 27002:2022 A.8.28
- OWASP Cheat Sheet Series, OWASP Proactive Controls, OWASP Top 10
- CWE Top 25

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
- **OWASP Cheat Sheet Series:** [OWASP Cheat Sheet Series Projektseite / OWASP Cheat Sheet Series project page](https://cheatsheetseries.owasp.org/)
- **OWASP Proactive Controls:** [OWASP Proactive Controls Projektseite / OWASP Proactive Controls project page](https://owasp.org/www-project-proactive-controls/)
- **OWASP Top 10 for LLM Applications:** [OWASP Top 10 for LLM Applications Projektseite / OWASP Top 10 for LLM Applications project page](https://owasp.org/www-project-top-10-for-large-language-model-applications/)
- **CWE Top 25:** [MITRE CWE Top 25 Übersicht / MITRE CWE Top 25 overview](https://cwe.mitre.org/top25/)

### Bewertung und Dokumentation / Assessment and Documentation

**DE:** Jeder Prüfpunkt bekommt genau einen Status. Schreibe die Begründung so, dass eine neue Kollegin oder ein neuer Kollege den Entscheid später ohne Rückfrage versteht.

**EN:** Each checklist item gets exactly one status. Write the explanation so that a new team member can understand the decision later without asking again.

- **Erfüllt / Fulfilled:** Die Anforderung ist umgesetzt und es gibt einen prüfbaren Nachweis.
- **Nicht erfüllt / Not fulfilled:** Die Anforderung ist noch nicht umgesetzt oder der Nachweis fehlt. Es muss eine Aufgabe mit verantwortlicher Person und Termin geben.
- **Nicht anwendbar / Not applicable:** Die Anforderung passt nicht zum Projekt. Das ist erlaubt, aber nur mit kurzer Begründung.

**Pflichtfelder je Prüfpunkt / Required fields per item:** Status, Begründung, Evidenzpfad oder Link, nächste Maßnahme mit verantwortlicher Person und Zieltermin.

### Durchführungshinweise / Implementation Guidance

**DE:** Nutze diese Checkliste nicht als reine Ja/Nein-Liste. Sie ist ein Arbeits- und Auditdokument. Prüfe jeden Punkt gegen reale Artefakte: Code, Pull Request, Architekturdiagramm, Build-Log, Scan-Ergebnis, Ticket, Betriebsdokumentation oder Freigabeprotokoll. Wenn ein Nachweis noch fehlt, markiere den Punkt als „nicht erfüllt" und lege eine konkrete Folgeaufgabe an.

**EN:** Do not use this checklist as a simple yes/no list. It is a working and audit document. Check each item against real artefacts: code, pull request, architecture diagram, build log, scan result, ticket, operations document, or approval record. If evidence is missing, mark the item as "not fulfilled" and create a concrete follow-up task.

**DE:** Schreibe kurze, klare Begründungen. Vermeide Abkürzungen ohne Erklärung. Wenn ein Punkt technisch schwierig ist, beschreibe den aktuellen Stand, das Risiko und den nächsten machbaren Schritt.

**EN:** Write short and clear explanations. Avoid unexplained abbreviations. If an item is technically difficult, describe the current state, the risk, and the next feasible step.

**DE:** Jeder Prüfpunkt muss deshalb drei Fragen beantworten: Was bedeutet die Anforderung im Projektalltag? Was ist konkret zu tun oder zu entscheiden? Welcher Nachweis zeigt das Ergebnis? Verwende Standard-IDs, Toolnamen und Abkürzungen nur zusammen mit einer kurzen Erklärung in Alltagssprache. Wenn ein Punkt für Auszubildende oder neue Teammitglieder nicht selbsterklärend ist, ergänze eine kurze Erklärung in der Begründung.

**EN:** Each item must therefore answer three questions: What does the requirement mean in daily project work? What exactly must be done or decided? Which evidence shows the result? Use standard IDs, tool names, and abbreviations only together with a short plain-language explanation. If an item is not self-explanatory for apprentices or new team members, add a short explanation in the rationale.

### Beispiel / Example

**DE:** Ein Pull Request ändert eine SQL-Abfrage. Die Review-Evidenz zeigt parametrisierte Queries, negative Tests gegen SQL-Injection und eine Fehlermeldung ohne interne Details. Wenn ein Test fehlt, wird der Punkt „nicht erfüllt" markiert und eine konkrete Nacharbeit mit verantwortlicher Person und Zieltermin angelegt.

**EN:** A pull request changes an SQL query. The review evidence shows parameterised queries, negative tests against SQL injection, and an error message without internal details. If a test is missing, the item is marked as "not fulfilled" and a concrete follow-up with owner and target date is created.

### A11Y-Hinweise / A11Y Notes

**DE:** Beim Ausfüllen dieser Checkliste müssen alle Nachweise auch textlich verständlich sein. Verweise sollen beschreibende Linktexte haben. Screenshots, Diagramme oder Scan-Auszüge brauchen eine kurze Textbeschreibung. Der Status darf nicht nur über Farbe erkennbar sein.

**EN:** When this checklist is filled in, all evidence must also be understandable as text. References should use descriptive link text. Screenshots, diagrams, or scan extracts need a short text description. The status must not be shown by color alone.

### Wichtige Begriffe / Key Terms

**DE:** Die folgenden Begriffe kommen in dieser Checkliste vor. Die Links springen zum Glossar dieses Kapitels, damit Auszubildende und Entwickler:innen ohne Sicherheits-Spezialwissen die Begriffe direkt nachlesen können.

**EN:** The following terms appear in this checklist. The links jump to this chapter's glossary so that apprentices and developers without specialist security knowledge can look them up directly.

- [Code Review](#cl-08-glossar-code-review)
- [Eingabevalidierung / Input Validation](#cl-08-glossar-input-validation)
- [Ausgabe-Codierung / Output Encoding](#cl-08-glossar-output-encoding)
- [Authentifizierung / Authentication](#cl-08-glossar-authentication)
- [Autorisierung / Authorisation](#cl-08-glossar-authorisation)
- [Sitzungsverwaltung / Session Management](#cl-08-glossar-session-management)
- [Secret Store](#cl-08-glossar-secret-store)
- [Logging und Audit / Logging and Audit](#cl-08-glossar-logging-audit)
- [Abhängigkeit / Dependency](#cl-08-glossar-dependency)
- [SAST](#cl-08-glossar-sast)
- [DAST](#cl-08-glossar-dast)
- [Linter](#cl-08-glossar-linter)
- [CodeQL](#cl-08-glossar-codeql)
- [Semgrep](#cl-08-glossar-semgrep)
- [SonarQube](#cl-08-glossar-sonarqube)
- [XSS / Cross-Site Scripting](#cl-08-glossar-xss)
- [SQL Injection](#cl-08-glossar-sql-injection)
- [Path Traversal](#cl-08-glossar-path-traversal)
- [Spec Kit](#cl-08-glossar-spec-kit)

### Allgemeine Punkte / General Items

#### 1. Eingabevalidierung / Input Validation

- **DE:** Alle Eingaben aus nicht vertrauenswürdigen Quellen (HTTP-Anfragen,
  Datei-Uploads, externe APIs, Message-Queues, Datenbankfelder mit Nutzer-
  inhalt) werden auf Typ, Länge, Format und Wertebereich geprüft. Validierung
  erfolgt am Vertrauensgrenzen-Übergang nach dem Allowlist-Prinzip
  (akzeptierte Werte definieren, nicht nur verbotene blockieren). Konkrete
  Bibliotheken: Java/Spring `@Valid` mit `jakarta.validation` (`@NotBlank`,
  `@Size`, `@Pattern`, `@Email`); Hibernate Validator für JSR-303/380;
  C#/.NET `System.ComponentModel.DataAnnotations` (`[Required]`,
  `[StringLength]`, `[RegularExpression]`, `[Range]`) mit `ModelState.IsValid`;
  FluentValidation für komplexe Regeln; Python `pydantic` v2 mit
  `BaseModel` und Type-Hints; `marshmallow`-Schemas; Go `validator/v10`
  (`validate:"required,email,max=255"`); JavaScript/TypeScript `zod`,
  `joi`, `yup`. Eingaben werden vor Verarbeitung kanonisiert (Unicode-
  Normalisierung NFC, Pfad-Normalisierung, Trimming). Maximale Größen für
  HTTP-Body (z. B. `MaxRequestBodySize` in ASP.NET Core, `client_max_body_size`
  in Nginx, `spring.servlet.multipart.max-file-size`) gesetzt. Referenzen:
  OWASP Input Validation Cheat Sheet, OWASP ASVS V5, CWE-20, CWE-1287.
- **EN:** All inputs from untrusted sources (HTTP requests, file uploads,
  external APIs, message queues, database fields with user content) are
  validated for type, length, format, and value range. Validation runs at
  the trust boundary using the allowlist principle (define accepted
  values, not only forbidden ones). Concrete libraries: Java/Spring
  `@Valid` with `jakarta.validation` (`@NotBlank`, `@Size`, `@Pattern`,
  `@Email`); Hibernate Validator for JSR-303/380; C#/.NET
  `System.ComponentModel.DataAnnotations` (`[Required]`,
  `[StringLength]`, `[RegularExpression]`, `[Range]`) with
  `ModelState.IsValid`; FluentValidation for complex rules; Python
  `pydantic` v2 with `BaseModel` and type hints; `marshmallow` schemas;
  Go `validator/v10` (`validate:"required,email,max=255"`);
  JavaScript/TypeScript `zod`, `joi`, `yup`. Inputs are canonicalised
  before processing (Unicode NFC, path normalisation, trimming). Maximum
  sizes for HTTP body (e.g. `MaxRequestBodySize` in ASP.NET Core,
  `client_max_body_size` in Nginx, `spring.servlet.multipart.max-file-size`)
  are set. References: OWASP Input Validation Cheat Sheet, OWASP ASVS V5,
  CWE-20, CWE-1287.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 2. Ausgabe-Codierung / Output Encoding

- **DE:** Ausgaben werden kontextgerecht codiert (HTML-Body, HTML-Attribut,
  JavaScript-String, URL-Parameter, CSS, SQL, LDAP, XML/XPath, Shell), um
  Injektionen (XSS, SQLi, LDAPi, OS-Command-Injection) zu verhindern.
  Konkrete Hilfsmittel: Java OWASP Encoder (`Encode.forHtml(...)`,
  `Encode.forHtmlAttribute(...)`, `Encode.forJavaScript(...)`,
  `Encode.forUriComponent(...)`); Spring `HtmlUtils.htmlEscape`; JSP/JSF
  Auto-Escaping in Templates (`<c:out>`, `EL ${value}`); C#/.NET
  `System.Web.HttpUtility.HtmlEncode`, `JavaScriptEncoder.Default.Encode`,
  `UrlEncoder.Default`, Razor `@variable` Auto-Escaping; Python
  `markupsafe.escape`, Jinja2 Auto-Escape (`autoescape=True`); Go
  `html/template` (kontextsensitives Auto-Escaping) statt `text/template`;
  JavaScript DOM-API (`textContent` statt `innerHTML`), Frameworks React/
  Vue/Angular escapen per Default in JSX/Templates (Vorsicht bei
  `dangerouslySetInnerHTML`, `v-html`, `[innerHTML]`). Content-Security-
  Policy (CSP) als zweite Verteidigungsschicht: `Content-Security-Policy:
  default-src 'self'; script-src 'self' 'nonce-{random}'`. SQL: ausschließ-
  lich Prepared Statements / parametrisierte Queries (siehe Punkt SQL).
  Referenzen: OWASP XSS Prevention Cheat Sheet, OWASP Output Encoding,
  CWE-79, CWE-89.
- **EN:** Outputs are encoded for the right context (HTML body, HTML
  attribute, JavaScript string, URL parameter, CSS, SQL, LDAP, XML/XPath,
  shell) to prevent injection (XSS, SQLi, LDAPi, OS command injection).
  Concrete helpers: Java OWASP Encoder (`Encode.forHtml(...)`,
  `Encode.forHtmlAttribute(...)`, `Encode.forJavaScript(...)`,
  `Encode.forUriComponent(...)`); Spring `HtmlUtils.htmlEscape`; JSP/JSF
  auto-escaping in templates (`<c:out>`, `EL ${value}`); C#/.NET
  `System.Web.HttpUtility.HtmlEncode`, `JavaScriptEncoder.Default.Encode`,
  `UrlEncoder.Default`, Razor `@variable` auto-escaping; Python
  `markupsafe.escape`, Jinja2 auto-escape (`autoescape=True`); Go
  `html/template` (context-aware auto-escaping) instead of `text/template`;
  JavaScript DOM API (`textContent` instead of `innerHTML`), frameworks
  React/Vue/Angular escape by default in JSX/templates (caution with
  `dangerouslySetInnerHTML`, `v-html`, `[innerHTML]`). Content Security
  Policy (CSP) as second defence layer: `Content-Security-Policy:
  default-src 'self'; script-src 'self' 'nonce-{random}'`. SQL:
  parameterised queries only (see SQL item). References: OWASP XSS
  Prevention Cheat Sheet, OWASP Output Encoding, CWE-79, CWE-89.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 3. Authentifizierung / Authentication

- **DE:** Es wird ein etabliertes Authentifizierungsverfahren genutzt
  (OAuth 2.1, OpenID Connect, SAML 2.0, WebAuthn/Passkeys, Kerberos für
  interne Domänen). Eigene Krypto- oder Passwort-Implementierungen werden
  vermieden. Konkrete Frameworks: Spring Security 6 (`SecurityFilterChain`
  mit `oauth2Login`, `formLogin`); Keycloak / Authentik / Auth0 / Azure AD /
  Entra ID als Identity Provider; ASP.NET Core Identity mit `Microsoft.
  AspNetCore.Authentication.JwtBearer` und `OpenIdConnect`; Python `Authlib`,
  `python-social-auth`, FastAPI `OAuth2PasswordBearer`; Node.js `passport.js`
  mit Strategien (`passport-oauth2`, `passport-saml`, `passport-jwt`).
  Passwort-Hashing nur mit modernen KDFs: Argon2id (RFC 9106) als bevorzugt
  (z. B. `argon2-cffi` Python, `Argon2.NET`, `argon2-jvm`); alternativ
  bcrypt mit cost ≥ 12 oder scrypt; PBKDF2-SHA256 mit ≥ 600 000 Iterationen
  (OWASP-Empfehlung 2024). Multi-Faktor-Authentifizierung (MFA) für
  privilegierte Zugänge Pflicht (TOTP per RFC 6238, FIDO2/WebAuthn). Brute-
  Force-Schutz: Account-Lockout nach 5 Fehlversuchen, Delay-basiertes
  Rate-Limiting (z. B. `bucket4j` Java, `slowapi` Python, `IpRateLimit`
  ASP.NET). Referenzen: OWASP Authentication Cheat Sheet, NIST SP 800-63B,
  OWASP ASVS V2, CWE-287, CWE-307.
- **EN:** An established authentication method is used (OAuth 2.1, OpenID
  Connect, SAML 2.0, WebAuthn/Passkeys, Kerberos for internal domains).
  Custom crypto or password implementations are avoided. Concrete
  frameworks: Spring Security 6 (`SecurityFilterChain` with `oauth2Login`,
  `formLogin`); Keycloak / Authentik / Auth0 / Azure AD / Entra ID as
  identity providers; ASP.NET Core Identity with
  `Microsoft.AspNetCore.Authentication.JwtBearer` and `OpenIdConnect`;
  Python `Authlib`, `python-social-auth`, FastAPI `OAuth2PasswordBearer`;
  Node.js `passport.js` with strategies (`passport-oauth2`,
  `passport-saml`, `passport-jwt`). Password hashing only with modern KDFs:
  Argon2id (RFC 9106) as preferred (e.g. `argon2-cffi` Python, `Argon2.NET`,
  `argon2-jvm`); alternatively bcrypt with cost ≥ 12 or scrypt;
  PBKDF2-SHA256 with ≥ 600,000 iterations (OWASP recommendation 2024).
  Multi-factor authentication (MFA) for privileged access mandatory (TOTP
  per RFC 6238, FIDO2/WebAuthn). Brute-force protection: account lockout
  after 5 failed attempts, delay-based rate limiting (e.g. `bucket4j` Java,
  `slowapi` Python, `IpRateLimit` ASP.NET). References: OWASP Authentication
  Cheat Sheet, NIST SP 800-63B, OWASP ASVS V2, CWE-287, CWE-307.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 4. Autorisierung / Authorisation

- **DE:** Berechtigungen werden serverseitig auf jeder geschützten Ressource
  und Aktion geprüft, nicht nur in der UI (Client-Code ist manipulierbar).
  Standardregel: Default-Deny — explizite Zulassung für jede Operation.
  Modelle: Role-Based Access Control (RBAC) für statische Rollen-
  zuweisungen, Attribute-Based Access Control (ABAC) für dynamische
  Regeln, Relationship-Based Access Control (ReBAC) für Multi-Tenant.
  Konkrete Implementierungen: Spring Security `@PreAuthorize("hasRole('ADMIN')
  and #user.id == authentication.principal.id")` und `@PostAuthorize`;
  ASP.NET Core `[Authorize(Policy = "AdminOrOwner")]` mit
  `IAuthorizationHandler` und `AuthorizationHandlerContext`; Python FastAPI
  Dependencies (`Depends(get_current_active_user)`) mit Custom Permission
  Decorators; Node.js `casbin` für RBAC/ABAC mit `enforce(sub, obj, act)`;
  Open Policy Agent (OPA) mit Rego-Policies für sprachunabhängige
  Autorisierung. Insecure Direct Object References (IDOR) verhindern:
  Ownership-Check vor Zugriff auf Ressource (z. B. `if document.owner_id !=
  current_user.id: abort(403)`); UUIDs statt sequentieller IDs für
  öffentliche URLs. Vertikale (Privilege Escalation) und horizontale
  (User-A greift auf User-B zu) Eskalation per Test abdecken. Referenzen:
  OWASP Authorization Cheat Sheet, OWASP ASVS V4, CWE-285, CWE-639,
  CWE-862, OWASP Top 10 A01:2021 Broken Access Control.
- **EN:** Permissions are checked on the server for every protected
  resource and action, not only in the UI (client code can be manipulated).
  Default rule: deny by default — explicit allow for every operation.
  Models: Role-Based Access Control (RBAC) for static role assignments,
  Attribute-Based Access Control (ABAC) for dynamic rules, Relationship-
  Based Access Control (ReBAC) for multi-tenant. Concrete implementations:
  Spring Security `@PreAuthorize("hasRole('ADMIN') and #user.id ==
  authentication.principal.id")` and `@PostAuthorize`; ASP.NET Core
  `[Authorize(Policy = "AdminOrOwner")]` with `IAuthorizationHandler` and
  `AuthorizationHandlerContext`; Python FastAPI dependencies
  (`Depends(get_current_active_user)`) with custom permission decorators;
  Node.js `casbin` for RBAC/ABAC with `enforce(sub, obj, act)`; Open Policy
  Agent (OPA) with Rego policies for language-agnostic authorization.
  Prevent Insecure Direct Object References (IDOR): ownership check
  before resource access (e.g. `if document.owner_id != current_user.id:
  abort(403)`); UUIDs instead of sequential IDs for public URLs. Cover
  vertical (privilege escalation) and horizontal (User-A accesses User-B)
  escalation by tests. References: OWASP Authorization Cheat Sheet, OWASP
  ASVS V4, CWE-285, CWE-639, CWE-862, OWASP Top 10 A01:2021 Broken Access
  Control.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 5. Sitzungsverwaltung / Session Management

- **DE:** Sitzungs-IDs werden mit kryptografisch sicheren Zufallsquellen
  erzeugt (CSPRNG), sind mindestens 128 Bit lang (Base64-codiert ≥ 22
  Zeichen, hex ≥ 32 Zeichen) und sind ablaufgesichert (absolute Lebenszeit
  und Idle-Timeout). Sie werden bei jedem Privilegienwechsel (Login,
  Logout, Passwort-Änderung, MFA-Aktivierung, Rollen-Eskalation) erneuert
  (Session Fixation verhindern). Konkrete Defaults: Spring Session
  (Redis/JDBC) mit `server.servlet.session.timeout=30m`,
  `server.servlet.session.cookie.secure=true`, `http-only=true`,
  `same-site=Lax`; ASP.NET Core `services.AddSession(options =>
  options.IdleTimeout = TimeSpan.FromMinutes(20))`,
  `Cookie.SecurePolicy = CookieSecurePolicy.Always`,
  `Cookie.HttpOnly = true`, `Cookie.SameSite = SameSiteMode.Lax`; Django
  `SESSION_COOKIE_SECURE = True`, `SESSION_COOKIE_HTTPONLY = True`,
  `SESSION_COOKIE_SAMESITE = 'Lax'`, `SESSION_EXPIRE_AT_BROWSER_CLOSE`;
  Express.js `express-session` mit `cookie: { secure: true, httpOnly: true,
  sameSite: 'lax', maxAge: 1800000 }`. Cookie-Flags Pflicht: `Secure`
  (nur über HTTPS), `HttpOnly` (kein JS-Zugriff), `SameSite=Lax` oder
  `Strict` (CSRF-Schutz). JWT als Bearer-Token: kurze Lebensdauer
  (15 min Access-Token, 7 Tage Refresh-Token mit Rotation), serverseitige
  Revocation-Liste oder JWE-Verschlüsselung sensitiver Claims, Algorithmus
  ES256 oder RS256 (kein `alg=none`, kein HS256 wenn Key-Sharing nötig).
  Logout invalidiert Session serverseitig (nicht nur Cookie löschen).
  Referenzen: OWASP Session Management Cheat Sheet, OWASP ASVS V3,
  CWE-384, CWE-613, CWE-1004.
- **EN:** Session IDs are generated with cryptographically secure random
  sources (CSPRNG), are at least 128 bits long (Base64-encoded ≥ 22
  characters, hex ≥ 32 characters), and have expiration (absolute lifetime
  and idle timeout). They are rotated on every privilege change (login,
  logout, password change, MFA activation, role escalation) to prevent
  session fixation. Concrete defaults: Spring Session (Redis/JDBC) with
  `server.servlet.session.timeout=30m`,
  `server.servlet.session.cookie.secure=true`, `http-only=true`,
  `same-site=Lax`; ASP.NET Core `services.AddSession(options =>
  options.IdleTimeout = TimeSpan.FromMinutes(20))`,
  `Cookie.SecurePolicy = CookieSecurePolicy.Always`,
  `Cookie.HttpOnly = true`, `Cookie.SameSite = SameSiteMode.Lax`; Django
  `SESSION_COOKIE_SECURE = True`, `SESSION_COOKIE_HTTPONLY = True`,
  `SESSION_COOKIE_SAMESITE = 'Lax'`, `SESSION_EXPIRE_AT_BROWSER_CLOSE`;
  Express.js `express-session` with `cookie: { secure: true, httpOnly:
  true, sameSite: 'lax', maxAge: 1800000 }`. Mandatory cookie flags:
  `Secure` (HTTPS only), `HttpOnly` (no JS access), `SameSite=Lax` or
  `Strict` (CSRF protection). JWT as bearer token: short lifetime (15 min
  access token, 7 days refresh token with rotation), server-side
  revocation list or JWE encryption of sensitive claims, algorithm ES256
  or RS256 (no `alg=none`, no HS256 if key sharing required). Logout
  invalidates session server-side (not only cookie deletion). References:
  OWASP Session Management Cheat Sheet, OWASP ASVS V3, CWE-384, CWE-613,
  CWE-1004.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 6. Kryptografie / Cryptography

- **DE:** Aktuelle Algorithmen werden verwendet: AES-256-GCM oder
  ChaCha20-Poly1305 für symmetrische Verschlüsselung (AEAD bevorzugt);
  RSA mindestens 3072 Bit oder ECDSA P-256/P-384 oder Ed25519 für
  Signaturen; ECDH P-256 oder X25519 für Schlüsselaustausch; SHA-256, SHA-
  384, SHA-512 oder SHA-3 für Hashing; HMAC-SHA-256 für Nachrichten-
  authentifizierung. Veraltete Verfahren — MD5, SHA-1 (für Signaturen und
  Zertifikate, weiterhin OK für HMAC mit Hinweis), DES/3DES, RC4, MD2 — sind
  verboten oder mit dokumentierter Risikobegründung in `docs/security/
  crypto-exceptions.md` markiert. Konkrete Bibliotheken: Java JCA mit
  Bouncy Castle Provider (`Cipher.getInstance("AES/GCM/NoPadding")`,
  `Signature.getInstance("Ed25519", "BC")`); Tink (Google) für High-Level
  API; C#/.NET `System.Security.Cryptography` (`AesGcm`, `RSACng`,
  `ECDsaCng`, `ECDiffieHellman`); Python `cryptography` Bibliothek
  (`Fernet` für symmetrisch, `hazmat.primitives` für Low-Level); Go
  `crypto/aes`, `crypto/cipher` (`cipher.NewGCM`), `crypto/ed25519`,
  `golang.org/x/crypto/chacha20poly1305`. TLS-Konfiguration: TLS 1.3 als
  Minimum (Fallback TLS 1.2 nur mit AEAD-Ciphern), HSTS-Header
  (`Strict-Transport-Security: max-age=31536000; includeSubDomains;
  preload`), OCSP Stapling. Schlüsselverwaltung: Hardware Security Module
  (HSM) PKCS#11, Cloud-KMS (AWS KMS, Azure Key Vault, GCP Cloud KMS) oder
  HashiCorp Vault. Schlüssel-Rotation alle 90 Tage für aktive Keys.
  Referenzen: NIST SP 800-131A Rev. 2, BSI TR-02102-1, OWASP Cryptographic
  Storage Cheat Sheet, ENISA Cryptographic Guidelines.
- **EN:** Current algorithms are used: AES-256-GCM or ChaCha20-Poly1305
  for symmetric encryption (AEAD preferred); RSA at least 3072 bits or
  ECDSA P-256/P-384 or Ed25519 for signatures; ECDH P-256 or X25519 for
  key exchange; SHA-256, SHA-384, SHA-512, or SHA-3 for hashing;
  HMAC-SHA-256 for message authentication. Deprecated methods — MD5, SHA-1
  (for signatures and certificates, still OK for HMAC with caveat),
  DES/3DES, RC4, MD2 — are forbidden or flagged with documented risk
  justification in `docs/security/crypto-exceptions.md`. Concrete
  libraries: Java JCA with Bouncy Castle Provider
  (`Cipher.getInstance("AES/GCM/NoPadding")`,
  `Signature.getInstance("Ed25519", "BC")`); Tink (Google) for high-level
  API; C#/.NET `System.Security.Cryptography` (`AesGcm`, `RSACng`,
  `ECDsaCng`, `ECDiffieHellman`); Python `cryptography` library (`Fernet`
  for symmetric, `hazmat.primitives` for low-level); Go `crypto/aes`,
  `crypto/cipher` (`cipher.NewGCM`), `crypto/ed25519`,
  `golang.org/x/crypto/chacha20poly1305`. TLS configuration: TLS 1.3 as
  minimum (TLS 1.2 fallback only with AEAD ciphers), HSTS header
  (`Strict-Transport-Security: max-age=31536000; includeSubDomains;
  preload`), OCSP Stapling. Key management: Hardware Security Module
  (HSM) PKCS#11, cloud KMS (AWS KMS, Azure Key Vault, GCP Cloud KMS), or
  HashiCorp Vault. Key rotation every 90 days for active keys. References:
  NIST SP 800-131A Rev. 2, BSI TR-02102-1, OWASP Cryptographic Storage
  Cheat Sheet, ENISA Cryptographic Guidelines.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 7. Geheimnisse / Secrets

- **DE:** Es liegen keine Schlüssel, API-Tokens, Passwörter, Connection-
  Strings, Private Keys, Cloud-Credentials oder Webhook-URLs im Code, in
  Konfigurationsdateien, Logs, Tests, Kommentaren oder Git-History.
  Geheimnisse werden ausschließlich aus plattformgeeigneten Secret-Stores
  gelesen: HashiCorp Vault, AWS Secrets Manager / Parameter Store, Azure
  Key Vault, GCP Secret Manager, Kubernetes Secrets (mit Sealed Secrets
  oder External Secrets Operator), GitHub Environments / Encrypted
  Secrets, Git CI/CD Variables (masked + protected). Lokal
  Entwicklungs-Secrets in `.env`-Dateien, die per `.gitignore`
  ausgeschlossen sind; oder OS-Credential-Stores (macOS Keychain, Windows
  Credential Manager, Linux Secret Service / `pass`). Secret-Scanner sind
  pre-commit und in CI aktiv: gitleaks (`gitleaks detect --redact`),
  trufflehog (`trufflehog filesystem . --no-update`), detect-secrets
  (`detect-secrets scan --baseline .secrets.baseline`), GitHub Secret
  Scanning + Push Protection, Git Secret Detection. Pre-commit-Hook in
  `.pre-commit-config.yaml`: `repos: - repo: https://github.com/gitleaks/
  gitleaks rev: v8.x hooks: - id: gitleaks`. Bei Verdacht auf Leak:
  sofortige Rotation des Geheimnisses, Audit-Log-Prüfung auf Missbrauch,
  Git-History-Bereinigung mit `git filter-repo` und Force-Push (nur nach
  Team-Abstimmung), Incident-Ticket. Referenzen: OWASP Secrets Management
  Cheat Sheet, CWE-798, CWE-321, CWE-540.
- **EN:** No keys, API tokens, passwords, connection strings, private
  keys, cloud credentials, or webhook URLs in code, configuration files,
  logs, tests, comments, or git history. Secrets are read exclusively from
  platform-appropriate secret stores: HashiCorp Vault, AWS Secrets Manager
  / Parameter Store, Azure Key Vault, GCP Secret Manager, Kubernetes
  Secrets (with Sealed Secrets or External Secrets Operator), GitHub
  Environments / Encrypted Secrets, Git CI/CD Variables (masked +
  protected). Local development secrets in `.env` files excluded via
  `.gitignore`; or OS credential stores (macOS Keychain, Windows
  Credential Manager, Linux Secret Service / `pass`). Secret scanners are
  active pre-commit and in CI: gitleaks (`gitleaks detect --redact`),
  trufflehog (`trufflehog filesystem . --no-update`), detect-secrets
  (`detect-secrets scan --baseline .secrets.baseline`), GitHub Secret
  Scanning + Push Protection, Git Secret Detection. Pre-commit hook in
  `.pre-commit-config.yaml`: `repos: - repo: https://github.com/gitleaks/
  gitleaks rev: v8.x hooks: - id: gitleaks`. On suspected leak: immediate
  rotation of the secret, audit-log review for misuse, git history cleanup
  with `git filter-repo` and force-push (only after team coordination),
  incident ticket. References: OWASP Secrets Management Cheat Sheet,
  CWE-798, CWE-321, CWE-540.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 8. Fehlerbehandlung / Error Handling

- **DE:** Fehlertexte für Endbenutzer enthalten keine Stack-Traces,
  Datenbank-Verbindungszeichenketten, internen Pfade, Versions-Strings,
  Debug-Informationen oder personenbezogene Daten anderer Nutzer. Generische
  Fehlermeldungen mit eindeutiger Korrelations-ID (Request-ID, Trace-ID),
  die im internen Log nachverfolgbar ist. Konkrete Implementierung: Spring
  Boot `@ControllerAdvice` mit `@ExceptionHandler` und `ProblemDetail`
  (RFC 9457); ASP.NET Core `IExceptionHandler` mit
  `ProblemDetailsFactory`, `app.UseExceptionHandler("/error")` (in
  Production keine Developer Exception Page); Python FastAPI
  `@app.exception_handler(Exception)` mit JSONResponse; Express.js
  Error-Middleware `app.use((err, req, res, next) => { logger.error(err);
  res.status(500).json({ error: 'Internal Error', traceId: req.id }); })`.
  HTTP-Status-Codes konsistent: 400 Bad Request für Validierungsfehler,
  401 Unauthorized für fehlende Authentifizierung, 403 Forbidden für
  fehlende Berechtigung, 404 Not Found (auch bei Authorisierungsfehler,
  um Existenz nicht zu verraten), 500 Internal Server Error für
  Server-Probleme. Production-Konfiguration: `ASPNETCORE_ENVIRONMENT=
  Production`, `spring.profiles.active=prod`, `DEBUG=False` (Django),
  `NODE_ENV=production`. Stack-Traces nur in internem Log mit erhöhter
  Schutzklasse (Audit-Log). Fail-Safe: bei Krypto- oder Auth-Fehlern keine
  Hinweise, ob z. B. Username oder Passwort falsch war (Constant-Time-
  Vergleich). Referenzen: OWASP Error Handling Cheat Sheet, CWE-209,
  CWE-754, CWE-755.
- **EN:** Error messages shown to end users contain no stack traces,
  database connection strings, internal paths, version strings, debug
  information, or other users' personal data. Generic error messages with
  a unique correlation ID (request ID, trace ID) that is traceable in the
  internal log. Concrete implementation: Spring Boot `@ControllerAdvice`
  with `@ExceptionHandler` and `ProblemDetail` (RFC 9457); ASP.NET Core
  `IExceptionHandler` with `ProblemDetailsFactory`,
  `app.UseExceptionHandler("/error")` (no Developer Exception Page in
  production); Python FastAPI `@app.exception_handler(Exception)` with
  JSONResponse; Express.js error middleware `app.use((err, req, res, next)
  => { logger.error(err); res.status(500).json({ error: 'Internal
  Error', traceId: req.id }); })`. Consistent HTTP status codes: 400 Bad
  Request for validation errors, 401 Unauthorized for missing
  authentication, 403 Forbidden for missing authorization, 404 Not Found
  (also for authorization errors, to not reveal existence), 500 Internal
  Server Error for server issues. Production configuration:
  `ASPNETCORE_ENVIRONMENT=Production`, `spring.profiles.active=prod`,
  `DEBUG=False` (Django), `NODE_ENV=production`. Stack traces only in
  internal log with increased protection class (audit log). Fail-safe: on
  crypto or auth errors no hints whether e.g. username or password was
  wrong (constant-time comparison). References: OWASP Error Handling
  Cheat Sheet, CWE-209, CWE-754, CWE-755.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 9. Logging und Audit / Logging and Audit

- **DE:** Sicherheitsrelevante Ereignisse werden geloggt: erfolgreiche und
  fehlgeschlagene Logins, Logout, Passwort-Änderungen, MFA-Aktivierung,
  Rechteänderungen, Rollen-Zuweisungen, Zugriffe auf sensible Ressourcen,
  Konfigurationsänderungen, Admin-Aktionen, Anomalien (Geo-Sprünge,
  ungewöhnliche User-Agents), kryptografische Schlüssel-Operationen,
  CRUD-Operationen auf personenbezogenen Daten (DSGVO Art. 30
  Verzeichnis). Pflichtfelder pro Log-Event: ISO-8601-Zeitstempel mit
  Zeitzone, Korrelations-ID (Trace-ID/Request-ID), Akteur (User-ID,
  Service-Account, Source-IP), Aktion, Ressource, Ergebnis (success/
  failure), Fehlerursache. Strukturierte Logs (JSON) statt Plaintext.
  Konkrete Tools: Java SLF4J + Logback mit `MDC.put("traceId", ...)`;
  C#/.NET `ILogger<T>` mit `Microsoft.Extensions.Logging` und
  `LogContext.PushProperty`; Python `structlog` oder `python-json-logger`;
  Go `slog` (stdlib seit 1.21) oder `zap`/`zerolog`; Node.js `pino` oder
  `winston`. Zentralisierung: ELK-Stack (Elasticsearch + Logstash +
  Kibana), Splunk, Grafana Loki, Datadog, Azure Monitor. Es werden keine
  Geheimnisse (Passwörter, Tokens, Session-IDs, Schlüssel, Kreditkarten-
  nummern) oder übermäßigen personenbezogenen Daten geloggt — Sanitisierung
  per Logger-Filter (z. B. `logback` `<MaskingFilter>`, Serilog
  `Destructure.ByMaskingProperties`). Aufbewahrung: 90 Tage operativ,
  1 Jahr Sicherheitsereignisse, 10 Jahre für CRA-Vorfälle. Integrität:
  WORM-Storage, S3 Object Lock, Hash-Chain für Audit-Logs.
  Referenzen: OWASP Logging Cheat Sheet, NIST SP 800-92, CWE-117,
  CWE-532, CWE-778.
- **EN:** Security-relevant events are logged: successful and failed
  logins, logout, password changes, MFA activation, permission changes,
  role assignments, access to sensitive resources, configuration changes,
  admin actions, anomalies (geo jumps, unusual user agents), cryptographic
  key operations, CRUD operations on personal data (GDPR Art. 30 register).
  Mandatory fields per log event: ISO-8601 timestamp with timezone,
  correlation ID (trace ID/request ID), actor (user ID, service account,
  source IP), action, resource, result (success/failure), error cause.
  Structured logs (JSON) instead of plaintext. Concrete tools: Java SLF4J
  + Logback with `MDC.put("traceId", ...)`; C#/.NET `ILogger<T>` with
  `Microsoft.Extensions.Logging` and `LogContext.PushProperty`; Python
  `structlog` or `python-json-logger`; Go `slog` (stdlib since 1.21) or
  `zap`/`zerolog`; Node.js `pino` or `winston`. Centralisation: ELK
  stack (Elasticsearch + Logstash + Kibana), Splunk, Grafana Loki,
  Datadog, Azure Monitor. No secrets (passwords, tokens, session IDs,
  keys, credit card numbers) or excess personal data are logged —
  sanitisation via logger filter (e.g. `logback` `<MaskingFilter>`,
  Serilog `Destructure.ByMaskingProperties`). Retention: 90 days
  operational, 1 year security events, 10 years for CRA incidents.
  Integrity: WORM storage, S3 Object Lock, hash chain for audit logs.
  References: OWASP Logging Cheat Sheet, NIST SP 800-92, CWE-117,
  CWE-532, CWE-778.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 10. Datei- und Netzwerk-I/O / File and Network I/O

- **DE:** Pfade werden gegen Path-Traversal (`../`-Sequenzen, absolute
  Pfade, Unicode-Tricks, NULL-Bytes) geschützt: Eingaben kanonisieren
  (`Path.toAbsolutePath().normalize()` Java, `Path.GetFullPath` C#,
  `os.path.realpath` Python, `filepath.Clean` Go), gegen erlaubtes Basis-
  Verzeichnis prüfen (`startsWith(BASE_DIR)`), Allowlist von
  Dateinamen-Mustern, Container/Chroot oder seccomp für Hard-Limits.
  URLs werden gegen Server-Side Request Forgery (SSRF) abgesichert:
  Allowlist erlaubter Hosts, Verbot von privaten IP-Ranges
  (RFC 1918: 10/8, 172.16/12, 192.168/16; Loopback 127/8; Link-Local
  169.254/16; Cloud-Metadata 169.254.169.254 AWS/Azure, metadata.google.
  internal GCP), DNS-Rebinding-Schutz (Resolved-IP nach DNS-Auflösung
  validieren), keine Redirects ohne Re-Validation. Bibliotheken: Java
  `java.net.http.HttpClient` mit Custom `Authenticator`, Python
  `requests` mit `allowed_hosts`, Go `net/http` mit Custom `Transport`,
  Node.js `ssrf-req-filter`. Up- und Downloads prüfen Größe (Maximum vor
  Empfang über `Content-Length`-Header und Streaming-Limit), MIME-Typ
  (Magic-Byte-Erkennung mit `Apache Tika`, `python-magic`, `file(1)`
  statt nur Header-Vertrauen), Dateierweiterung-Allowlist, Antivirus-Scan
  (ClamAV, VirusTotal API), Speicherung außerhalb des Webroots oder mit
  zufälligen Dateinamen, Image-Stripping von Metadaten (EXIF) und
  Re-Encoding (ImageMagick mit Policy gegen RCE-CVEs). XML/JSON Parser:
  XXE-Schutz (`DocumentBuilderFactory.setFeature("http://apache.org/xml/
  features/disallow-doctype-decl", true)` Java; `defusedxml` Python),
  JSON-Bomben-Schutz (Tiefen- und Größenlimit). Referenzen: OWASP File
  Upload Cheat Sheet, OWASP SSRF Cheat Sheet, CWE-22, CWE-918, CWE-434,
  CWE-611.
- **EN:** Paths are protected against path traversal (`../` sequences,
  absolute paths, Unicode tricks, NULL bytes): canonicalise inputs
  (`Path.toAbsolutePath().normalize()` Java, `Path.GetFullPath` C#,
  `os.path.realpath` Python, `filepath.Clean` Go), check against allowed
  base directory (`startsWith(BASE_DIR)`), allowlist of filename patterns,
  container/chroot or seccomp for hard limits. URLs are protected against
  Server-Side Request Forgery (SSRF): allowlist of allowed hosts,
  prohibition of private IP ranges (RFC 1918: 10/8, 172.16/12, 192.168/16;
  loopback 127/8; link-local 169.254/16; cloud metadata 169.254.169.254
  AWS/Azure, metadata.google.internal GCP), DNS rebinding protection
  (validate resolved IP after DNS resolution), no redirects without
  re-validation. Libraries: Java `java.net.http.HttpClient` with custom
  `Authenticator`, Python `requests` with `allowed_hosts`, Go `net/http`
  with custom `Transport`, Node.js `ssrf-req-filter`. Uploads and
  downloads check size (maximum before receiving via `Content-Length`
  header and streaming limit), MIME type (magic-byte detection with
  `Apache Tika`, `python-magic`, `file(1)` instead of trusting headers
  only), file-extension allowlist, antivirus scan (ClamAV, VirusTotal
  API), storage outside the webroot or with random filenames, image
  metadata stripping (EXIF) and re-encoding (ImageMagick with policy
  against RCE CVEs). XML/JSON parser: XXE protection
  (`DocumentBuilderFactory.setFeature("http://apache.org/xml/features/
  disallow-doctype-decl", true)` Java; `defusedxml` Python), JSON-bomb
  protection (depth and size limits). References: OWASP File Upload Cheat
  Sheet, OWASP SSRF Cheat Sheet, CWE-22, CWE-918, CWE-434, CWE-611.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 11. Abhängigkeiten / Dependencies

- **DE:** Neue Abhängigkeiten sind aktiv gepflegt (letzte Veröffentlichung
  ≤ 12 Monate, keine archivierten Repositories), ohne bekannte kritische
  CVEs (CVSS ≥ 9.0) oder Hochstufungen, mit erlaubter Lizenz und
  vertrauenswürdigem Maintainer-Track-Record. Lock-Dateien sind committet
  und aktualisiert: `package-lock.json`/`yarn.lock`/`pnpm-lock.yaml`,
  `poetry.lock`/`uv.lock`/`Pipfile.lock`/`requirements.txt` mit Hashes
  (`pip install --require-hashes`), `Cargo.lock`, `go.sum`, `Gemfile.lock`,
  `composer.lock`, `packages.lock.json` (NuGet mit `RestorePackagesWithLockFile`).
  Strict-Mode in CI: `npm ci`, `yarn install --frozen-lockfile`, `pnpm
  install --frozen-lockfile`, `cargo build --locked`, `go mod verify`.
  Container-Images mit Digest-Pinning (`@sha256:...`) statt mutable Tags.
  CVE-Scanning bei jedem Build und PR: GitHub Dependabot Alerts,
  Git Dependency Scanning, Snyk, Mend (WhiteSource), JFrog Xray, OWASP
  Dependency-Check, OSV-Scanner (Google), Trivy, Grype. SBOM-Generierung
  pro Build (CycloneDX 1.5+ oder SPDX 2.3+) — siehe CL_Lieferkette-Build-
  Integritaet. Konkrete Quellen: NVD (`nvd.nist.gov`), OSV.dev, GitHub
  Security Advisories (GHSA), CISA Known Exploited Vulnerabilities (KEV),
  EPSS-Score (Exploit Prediction Scoring System). Update-Strategie:
  Renovate oder Dependabot mit `minimumReleaseAge: "3 days"` (Cooldown
  gegen Supply-Chain-Angriffe), Auto-Merge nur für Patch-Updates mit
  grünem CI; Major-Updates manuell mit Changelog-Review. Verbotene
  Quellen: typosquatted Pakete, gelöschte/verlassene Maintainer,
  unbestätigte Mirror-Repos. Referenzen: OWASP Top 10 A06:2021 Vulnerable
  and Outdated Components, CWE-1104, CWE-1395, NIST SSDF PW.4.
- **EN:** New dependencies are actively maintained (last release ≤ 12
  months, no archived repositories), free of known critical CVEs (CVSS
  ≥ 9.0) or upgrade flags, with allowed license and trusted maintainer
  track record. Lock files are committed and up to date:
  `package-lock.json`/`yarn.lock`/`pnpm-lock.yaml`,
  `poetry.lock`/`uv.lock`/`Pipfile.lock`/`requirements.txt` with hashes
  (`pip install --require-hashes`), `Cargo.lock`, `go.sum`,
  `Gemfile.lock`, `composer.lock`, `packages.lock.json` (NuGet with
  `RestorePackagesWithLockFile`). Strict mode in CI: `npm ci`,
  `yarn install --frozen-lockfile`, `pnpm install --frozen-lockfile`,
  `cargo build --locked`, `go mod verify`. Container images with digest
  pinning (`@sha256:...`) instead of mutable tags. CVE scanning on every
  build and PR: GitHub Dependabot Alerts, Git Dependency Scanning,
  Snyk, Mend (WhiteSource), JFrog Xray, OWASP Dependency-Check,
  OSV-Scanner (Google), Trivy, Grype. SBOM generation per build
  (CycloneDX 1.5+ or SPDX 2.3+) — see CL_Lieferkette-Build-Integritaet.
  Concrete sources: NVD (`nvd.nist.gov`), OSV.dev, GitHub Security
  Advisories (GHSA), CISA Known Exploited Vulnerabilities (KEV), EPSS
  score (Exploit Prediction Scoring System). Update strategy: Renovate or
  Dependabot with `minimumReleaseAge: "3 days"` (cooldown against
  supply-chain attacks), auto-merge only for patch updates with green CI;
  major updates manual with changelog review. Forbidden sources:
  typosquatted packages, deleted/abandoned maintainers, unverified mirror
  repos. References: OWASP Top 10 A06:2021 Vulnerable and Outdated
  Components, CWE-1104, CWE-1395, NIST SSDF PW.4.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 12. Tests / Tests

- **DE:** Sicherheitsrelevante Pfade (Authentifizierung, Autorisierung,
  Eingabevalidierung, Ausgabe-Codierung, Krypto-Operationen, Session-
  Verwaltung, Fehlerbehandlung, File/Network-I/O) haben automatisierte
  Tests in zwei Varianten: positive Fälle (erwartetes Verhalten bei
  zulässiger Eingabe) und negative Fälle (Ablehnung bei Angriffsversuchen,
  Boundary-Werten, NULL/leeren Werten, Unicode-Edge-Cases). Test-
  Frameworks: Java JUnit 5 + Mockito + AssertJ + REST Assured; Spring
  Boot `@WebMvcTest` und `@SpringBootTest`; C#/.NET xUnit + Moq + FluentAssertions
  + WebApplicationFactory; Python pytest + pytest-mock + httpx; Go
  `testing` + testify; Node.js Jest + supertest. Security-spezifische
  Tools: OWASP ZAP für DAST in CI (`zap-baseline.py`), Burp Suite für
  manuelle Pentests, Semgrep für SAST mit OWASP Top 10 Rules
  (`semgrep --config p/owasp-top-ten`), CodeQL für GitHub-native SAST,
  SonarQube/SonarCloud, Snyk Code, Checkmarx. Coverage-Minimum für
  sicherheitskritische Module: mindestens 85 % oder besser Branch
  Coverage. Integrationstests decken mindestens 80 % oder besser der
  dokumentierten öffentlichen Schnittstellen (zum Beispiel REST APIs,
  CLI-Kommandos oder vergleichbare externe Schnittstellen) und mindestens
  80 % oder besser der dokumentierten kritischen UI-Flows ab. Jede neue
  oder geänderte öffentliche Schnittstelle hat mindestens einen positiven
  und einen negativen Integrationstest. Property-Based Testing für
  Validierung (Java `jqwik`, Python `hypothesis`, Go `testing/quick`);
  Fuzz-Testing für Parser und Eingabeverarbeitung (Go-Fuzz, libFuzzer,
  Atheris für Python, Jazzer für Java). Beispiele
  für negative Tests: SQL-Injection-Strings (`' OR 1=1--`), XSS-Payloads
  (`<script>alert(1)</script>`), Path-Traversal (`../../etc/passwd`),
  Buffer-Overflow-Strings (10 000 Zeichen), Authentifizierung mit
  ungültigem Token, Autorisierung mit fremder User-ID. Continuous Security
  Testing: GitHub Actions Workflow mit SAST + SCA + DAST + Secret Scanning
  bei jedem PR. Referenzen: OWASP Testing Guide v4.2, OWASP ASVS V12,
  NIST SP 800-53 SA-11, ISO/IEC 27001 A.8.29.
- **EN:** Security-relevant paths (authentication, authorization, input
  validation, output encoding, crypto operations, session management,
  error handling, file/network I/O) have automated tests in two variants:
  positive cases (expected behaviour on valid input) and negative cases
  (rejection on attack attempts, boundary values, NULL/empty values,
  Unicode edge cases). Test frameworks: Java JUnit 5 + Mockito +
  AssertJ + REST Assured; Spring Boot `@WebMvcTest` and `@SpringBootTest`;
  C#/.NET xUnit + Moq + FluentAssertions + WebApplicationFactory; Python
  pytest + pytest-mock + httpx; Go `testing` + testify; Node.js Jest +
  supertest. Security-specific tools: OWASP ZAP for DAST in CI
  (`zap-baseline.py`), Burp Suite for manual pentest, Semgrep for SAST
  with OWASP Top 10 rules (`semgrep --config p/owasp-top-ten`), CodeQL
  for GitHub-native SAST, SonarQube/SonarCloud, Snyk Code, Checkmarx.
  Coverage minimum for security-critical modules: at least 85 % or
  better branch coverage. Integration tests cover at least 80 % or
  better of the documented public interfaces (for example REST APIs,
  CLI commands, or comparable external interfaces) and at least 80 % or
  better of the documented critical UI flows. Every new or changed
  public interface has at least one positive and one negative integration
  test. Property-based testing for validation (Java `jqwik`, Python
  `hypothesis`, Go `testing/quick`); fuzz testing for parsers and input
  processing (Go-Fuzz, libFuzzer, Atheris for Python, Jazzer for Java).
  Examples of negative tests: SQL injection strings (`' OR 1=1--`), XSS
  payloads (`<script>alert(1)</script>`), path traversal
  (`../../etc/passwd`), buffer-overflow strings (10,000 characters),
  authentication with invalid token, authorization with foreign user ID.
  Continuous security testing: GitHub Actions workflow with SAST + SCA +
  DAST + secret scanning on every PR. References: OWASP Testing Guide
  v4.2, OWASP ASVS V12, NIST SP 800-53 SA-11, ISO/IEC 27001 A.8.29.
- **Akzeptanz / Acceptance:** Coverage-Bericht zeigt mindestens 85 %
  oder besser Branch Coverage für sicherheitskritische Module; Test-
  oder Traceability-Report zeigt mindestens 80 % oder besser Abdeckung
  der dokumentierten öffentlichen Schnittstellen und kritischen UI-Flows;
  jede neue oder geänderte öffentliche Schnittstelle hat mindestens
  einen positiven und einen negativen Integrationstest. / Coverage report
  shows at least 85 % or better branch coverage for security-critical
  modules; test or traceability report shows at least 80 % or better
  coverage of documented public interfaces and critical UI flows; every
  new or changed public interface has at least one positive and one
  negative integration test.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

### Sprachspezifische Punkte / Language-Specific Items

#### Java / Java

- **DE:** Spring Security 6 oder gleichwertig (Apache Shiro, Quarkus
  Security) korrekt konfiguriert: explizite `SecurityFilterChain`-Bean,
  `authorizeHttpRequests` mit Default-Deny, CSRF-Schutz aktiv für Web
  (`http.csrf(Customizer.withDefaults())` mit `CookieCsrfTokenRepository`).
  SQL: `Statement` durch `PreparedStatement` mit `?`-Platzhaltern ersetzt;
  JPA/Hibernate mit JPQL-Parametern (`:param`) statt String-Konkatenation;
  jOOQ als typsicheres Builder-Framework. Sichere Deserialisierung: kein
  blindes `ObjectInputStream`; `ValidatingObjectInputStream` mit Allowlist
  (`apache-commons-io`); JSON-Deserialisierung mit Jackson und
  `@JsonTypeInfo` ohne `Default Typing` (`ObjectMapper.activateDefaultTyping`
  vermeiden); SnakeYAML mit `SafeConstructor`; XML mit
  `disallow-doctype-decl=true`. Build mit Java 21 LTS; `--enable-preview`
  nur für Tests; Pattern Matching, Records und sealed Classes für sichere
  Datentypen. Statische Analyse: SpotBugs + FindSecBugs Plugin, Error
  Prone, PMD mit Security Ruleset, SonarQube. Beispiel sichere Query:
  `var stmt = conn.prepareStatement("SELECT * FROM users WHERE email = ?");
  stmt.setString(1, email);`.
- **EN:** Spring Security 6 or equivalent (Apache Shiro, Quarkus Security)
  configured correctly: explicit `SecurityFilterChain` bean,
  `authorizeHttpRequests` with default-deny, CSRF protection on for web
  (`http.csrf(Customizer.withDefaults())` with `CookieCsrfTokenRepository`).
  SQL: `Statement` replaced by `PreparedStatement` with `?` placeholders;
  JPA/Hibernate with JPQL parameters (`:param`) instead of string
  concatenation; jOOQ as type-safe builder framework. Safe deserialisation:
  no blind `ObjectInputStream`; `ValidatingObjectInputStream` with
  allowlist (`apache-commons-io`); JSON deserialisation with Jackson and
  `@JsonTypeInfo` without default typing (avoid
  `ObjectMapper.activateDefaultTyping`); SnakeYAML with `SafeConstructor`;
  XML with `disallow-doctype-decl=true`. Build with Java 21 LTS;
  `--enable-preview` only for tests; pattern matching, records, and sealed
  classes for safe data types. Static analysis: SpotBugs + FindSecBugs
  plugin, Error Prone, PMD with security ruleset, SonarQube. Example safe
  query: `var stmt = conn.prepareStatement("SELECT * FROM users WHERE
  email = ?"); stmt.setString(1, email);`.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### C# / .NET / C# / .NET

- **DE:** Parametrisierte Datenbankzugriffe per ADO.NET
  (`SqlCommand.Parameters.AddWithValue`/`Add` mit `SqlDbType`), Dapper
  (`connection.Query<User>("SELECT ... WHERE id = @id", new { id })`),
  Entity Framework Core mit LINQ und `FromSqlInterpolated` (sicher) statt
  `FromSqlRaw` (unsicher bei String-Konkatenation). Anti-Forgery-Token in
  Web: ASP.NET Core `services.AddAntiforgery()`, `[ValidateAntiForgeryToken]`
  auf Controller-Actions, Razor `@Html.AntiForgeryToken()` in Forms,
  Header `X-XSRF-TOKEN` für SPAs. Sichere JSON-Deserialisierung mit
  `System.Text.Json` (`JsonSerializerOptions { PropertyNameCaseInsensitive
  = true, AllowTrailingCommas = false }`); bei `Newtonsoft.Json`
  `TypeNameHandling = None` setzen (CVE-2017-9785, CVE-2020-7060). XML
  mit `XmlReaderSettings { DtdProcessing = DtdProcessing.Prohibit,
  XmlResolver = null }` gegen XXE. `HttpClient` als Singleton oder per
  `IHttpClientFactory` (Socket-Erschöpfung vermeiden); Polly für
  Retry/Circuit-Breaker. `Set-StrictMode -Version Latest` in PowerShell-
  Hilfsskripten. Async/await durchgängig (kein `.Result`/`.Wait()` —
  Deadlock-Gefahr); `CancellationToken` propagieren. Nullable Reference
  Types aktivieren (`<Nullable>enable</Nullable>` in `.csproj`).
  Logging mit `ILogger<T>` und Source-Generators (kein String-Format mit
  User-Input). Build: .NET 8 LTS oder neuer; SDK-Style `.csproj` mit
  `<TreatWarningsAsErrors>true</TreatWarningsAsErrors>`; Roslyn-Analyzer
  `Microsoft.CodeAnalysis.NetAnalyzers` mit Security-Regeln (CA5xxx),
  Security Code Scan, SonarAnalyzer.
- **EN:** Parameterised database access via ADO.NET
  (`SqlCommand.Parameters.AddWithValue`/`Add` with `SqlDbType`), Dapper
  (`connection.Query<User>("SELECT ... WHERE id = @id", new { id })`),
  Entity Framework Core with LINQ and `FromSqlInterpolated` (safe) instead
  of `FromSqlRaw` (unsafe with string concatenation). Anti-forgery tokens
  in web: ASP.NET Core `services.AddAntiforgery()`,
  `[ValidateAntiForgeryToken]` on controller actions, Razor
  `@Html.AntiForgeryToken()` in forms, header `X-XSRF-TOKEN` for SPAs.
  Safe JSON deserialisation with `System.Text.Json`
  (`JsonSerializerOptions { PropertyNameCaseInsensitive = true,
  AllowTrailingCommas = false }`); with `Newtonsoft.Json` set
  `TypeNameHandling = None` (CVE-2017-9785, CVE-2020-7060). XML with
  `XmlReaderSettings { DtdProcessing = DtdProcessing.Prohibit,
  XmlResolver = null }` against XXE. `HttpClient` as singleton or via
  `IHttpClientFactory` (avoid socket exhaustion); Polly for
  retry/circuit-breaker. `Set-StrictMode -Version Latest` in PowerShell
  helper scripts. Async/await throughout (no `.Result`/`.Wait()` —
  deadlock risk); propagate `CancellationToken`. Enable nullable reference
  types (`<Nullable>enable</Nullable>` in `.csproj`). Logging with
  `ILogger<T>` and source generators (no string format with user input).
  Build: .NET 8 LTS or newer; SDK-style `.csproj` with
  `<TreatWarningsAsErrors>true</TreatWarningsAsErrors>`; Roslyn analyzer
  `Microsoft.CodeAnalysis.NetAnalyzers` with security rules (CA5xxx),
  Security Code Scan, SonarAnalyzer.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### Python / Python

- **DE:** Bibliothek `cryptography` (PyCA, mit moderner High-Level-API
  `Fernet`, Low-Level `hazmat.primitives`) statt eigener Krypto-
  Implementierung oder veralteter `pycrypto`-Fork. Sichere Parser:
  `defusedxml` für XML (verhindert XXE, Billion Laughs, External Entity
  Injection); `defusedjson` für sichere JSON-Tiefe; `ruamel.yaml` mit
  `safe_load` oder `yaml.safe_load` (kein `yaml.load` ohne Loader).
  Kein `eval`, `exec`, `compile` oder `pickle.loads` auf nicht
  vertrauenswürdigen Eingaben (RCE-Risiko); Alternativen: `ast.literal_eval`
  für Literale, JSON, msgpack, Protocol Buffers für Datenaustausch.
  `subprocess` ohne `shell=True` für nicht vertrauenswürdige Eingaben:
  `subprocess.run([cmd, arg1, arg2], capture_output=True, text=True,
  check=True)` mit Listen-Argumenten; bei Bedarf `shlex.quote(arg)`.
  HTTP-Requests mit `requests` und `verify=True` (Default), Timeout setzen
  (`timeout=10`); SSRF-Schutz mit Allowlist. Frameworks: FastAPI mit
  `pydantic` v2 für Validierung, Starlette mit
  `SecurityHeaders`-Middleware; Django mit `SecurityMiddleware`,
  `CsrfViewMiddleware`, ORM (`Model.objects.filter` statt rohem SQL).
  Type-Hints (`mypy --strict`) und `ruff` Linter mit Security-Rules
  (`bandit`-Integration). Build: Python 3.12+ (3.13 bevorzugt); virtuelle
  Umgebungen (`venv`, `uv`, `poetry`); Lock-File `requirements.txt` mit
  `pip-compile --generate-hashes` oder `uv lock`. Statische Analyse:
  bandit, semgrep, ruff, mypy, pylint mit Security-Plugin.
- **EN:** `cryptography` library (PyCA, with modern high-level API
  `Fernet`, low-level `hazmat.primitives`) instead of custom crypto
  implementation or deprecated `pycrypto` fork. Safe parsers:
  `defusedxml` for XML (prevents XXE, billion laughs, external entity
  injection); `defusedjson` for safe JSON depth; `ruamel.yaml` with
  `safe_load` or `yaml.safe_load` (no `yaml.load` without loader). No
  `eval`, `exec`, `compile`, or `pickle.loads` on untrusted inputs (RCE
  risk); alternatives: `ast.literal_eval` for literals, JSON, msgpack,
  Protocol Buffers for data exchange. `subprocess` without `shell=True`
  for untrusted inputs: `subprocess.run([cmd, arg1, arg2],
  capture_output=True, text=True, check=True)` with list arguments;
  `shlex.quote(arg)` if needed. HTTP requests with `requests` and
  `verify=True` (default), set timeout (`timeout=10`); SSRF protection
  with allowlist. Frameworks: FastAPI with `pydantic` v2 for validation,
  Starlette with `SecurityHeaders` middleware; Django with
  `SecurityMiddleware`, `CsrfViewMiddleware`, ORM (`Model.objects.filter`
  instead of raw SQL). Type hints (`mypy --strict`) and `ruff` linter with
  security rules (`bandit` integration). Build: Python 3.12+ (3.13
  preferred); virtual environments (`venv`, `uv`, `poetry`); lock file
  `requirements.txt` with `pip-compile --generate-hashes` or `uv lock`.
  Static analysis: bandit, semgrep, ruff, mypy, pylint with security
  plugin.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### Go / Go

- **DE:** Kontextgebundene HTTP-Templates (`html/template`,
  kontextsensitives Auto-Escaping pro Output-Stelle: HTML-Body, Attribut,
  JS, URL, CSS) statt `text/template` für HTML-Output. `database/sql` mit
  Platzhaltern (`db.QueryContext(ctx, "SELECT * FROM users WHERE id = $1",
  id)`) statt `fmt.Sprintf` mit User-Input; ORM-Bibliotheken `sqlc`
  (typsicher, Code-Generator), `gorm` mit `Where("name = ?", name)` oder
  `ent` (Facebook). `crypto/rand` (CSPRNG) statt `math/rand` für
  Sicherheits-Token, Session-IDs, Salts (`b := make([]byte, 32);
  crypto/rand.Read(b)`). `math/rand/v2` (Go 1.22+) nur für nicht-
  sicherheitsrelevante Zufallswerte. Race-Detector im Test:
  `go test -race ./...` in CI Pflicht (Datenrennen-Bugs sind häufig
  Sicherheitslücken). Web-Frameworks: `net/http` mit
  `http.TimeoutHandler` und `Server.ReadTimeout`/`WriteTimeout`/`IdleTimeout`
  gegen Slowloris; `chi`, `echo`, `gin`, `fiber` mit Security-
  Middleware (CSRF, CORS, Security Headers). Kontext-Propagation
  (`context.Context`) für Cancellation und Deadlines durchgängig. Statische
  Analyse: `go vet`, `staticcheck`, `gosec` (Security-Linter mit Regeln
  G101-G505), `golangci-lint` mit `gosec`-Plugin. Build: Go 1.22 oder
  neuer; `go.sum` committen; `go mod verify` in CI; `go build -trimpath`
  (Pfade aus Binary entfernen); `-buildmode=pie` für ASLR. Verbotene
  Pakete: `unsafe` ohne dokumentierte Begründung, `os/exec` mit `Shell`-
  Wrapper, eigene HTTP-Parser.
- **EN:** Context-aware HTML templates (`html/template`, context-sensitive
  auto-escaping per output position: HTML body, attribute, JS, URL, CSS)
  instead of `text/template` for HTML output. `database/sql` with
  placeholders (`db.QueryContext(ctx, "SELECT * FROM users WHERE id = $1",
  id)`) instead of `fmt.Sprintf` with user input; ORM libraries `sqlc`
  (type-safe, code generator), `gorm` with `Where("name = ?", name)`, or
  `ent` (Facebook). `crypto/rand` (CSPRNG) instead of `math/rand` for
  security tokens, session IDs, salts (`b := make([]byte, 32);
  crypto/rand.Read(b)`). `math/rand/v2` (Go 1.22+) only for
  non-security-relevant random values. Race detector in tests:
  `go test -race ./...` mandatory in CI (data races are often security
  bugs). Web frameworks: `net/http` with `http.TimeoutHandler` and
  `Server.ReadTimeout`/`WriteTimeout`/`IdleTimeout` against slowloris;
  `chi`, `echo`, `gin`, `fiber` with security middleware (CSRF, CORS,
  security headers). Context propagation (`context.Context`) for
  cancellation and deadlines throughout. Static analysis: `go vet`,
  `staticcheck`, `gosec` (security linter with rules G101-G505),
  `golangci-lint` with `gosec` plugin. Build: Go 1.22 or newer; commit
  `go.sum`; `go mod verify` in CI; `go build -trimpath` (remove paths
  from binary); `-buildmode=pie` for ASLR. Forbidden packages: `unsafe`
  without documented justification, `os/exec` with shell wrapper, custom
  HTTP parsers.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### Swift / Swift

- **DE:** Keychain-Services (`Security.framework`) für Geheimnisse mit
  passenden Accessibility-Klassen
  (`kSecAttrAccessibleAfterFirstUnlockThisDeviceOnly` für Server-Tokens,
  `kSecAttrAccessibleWhenUnlockedThisDeviceOnly` für interaktive
  Daten); kein `UserDefaults` für Geheimnisse. `URLSession` mit
  korrekter TLS-Konfiguration: ATS (App Transport Security) aktiv lassen;
  TLS 1.3 minimum
  (`URLSessionConfiguration.tlsMinimumSupportedProtocolVersion = .TLSv13`);
  Public Key Pinning oder Certificate Pinning per
  `URLSessionDelegate.urlSession(_:didReceive:completionHandler:)` für
  kritische Endpunkte; Hostname-Validierung nicht deaktivieren. Speicher-
  sichere Initialisierungen für Strings und Daten (Swift ist standardmäßig
  speichersicher; Vorsicht bei Bridging zu C-APIs): `Data` und `String`
  statt `UnsafeMutableBufferPointer`; bei Schlüsseln/Passwörtern explizit
  zero-out nach Gebrauch (`data.resetBytes(in: 0..<data.count)`); kein
  blindes `unsafeBitCast` oder Pointer-Arithmetik. Crypto:
  `CryptoKit` (iOS 13+, macOS 10.15+) statt CommonCrypto; `SymmetricKey`,
  `ChaChaPoly.seal`, `Curve25519.Signing` für moderne Algorithmen.
  Concurrency: `async/await` und Actors statt manueller Locks; `Sendable`-
  Protokolle für Thread-Safety. Frameworks: SwiftUI mit `@AppStorage`
  (kein Secret-Speicher), Combine; Vapor für Server-Side Swift mit
  Security-Middleware. Build: Swift 5.9+ oder Swift 6 mit `Strict
  Concurrency Checking`; SwiftLint mit Security-Regeln; Xcode Static
  Analyzer; Address Sanitizer + Thread Sanitizer in Tests.
- **EN:** Keychain services (`Security.framework`) for secrets with
  appropriate accessibility classes
  (`kSecAttrAccessibleAfterFirstUnlockThisDeviceOnly` for server tokens,
  `kSecAttrAccessibleWhenUnlockedThisDeviceOnly` for interactive data);
  no `UserDefaults` for secrets. `URLSession` with correct TLS
  configuration: leave ATS (App Transport Security) on; TLS 1.3 minimum
  (`URLSessionConfiguration.tlsMinimumSupportedProtocolVersion = .TLSv13`);
  public key pinning or certificate pinning via
  `URLSessionDelegate.urlSession(_:didReceive:completionHandler:)` for
  critical endpoints; do not disable hostname validation. Memory-safe
  initialisation for strings and data (Swift is memory-safe by default;
  caution when bridging to C APIs): `Data` and `String` instead of
  `UnsafeMutableBufferPointer`; for keys/passwords explicitly zero out
  after use (`data.resetBytes(in: 0..<data.count)`); no blind
  `unsafeBitCast` or pointer arithmetic. Crypto: `CryptoKit` (iOS 13+,
  macOS 10.15+) instead of CommonCrypto; `SymmetricKey`, `ChaChaPoly.seal`,
  `Curve25519.Signing` for modern algorithms. Concurrency: `async/await`
  and actors instead of manual locks; `Sendable` protocols for thread
  safety. Frameworks: SwiftUI with `@AppStorage` (no secret store),
  Combine; Vapor for server-side Swift with security middleware. Build:
  Swift 5.9+ or Swift 6 with strict concurrency checking; SwiftLint with
  security rules; Xcode Static Analyzer; Address Sanitizer + Thread
  Sanitizer in tests.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### SQL / SQL

- **DE:** Keine dynamischen SQL-Strings aus Benutzereingaben — niemals
  String-Konkatenation, `String.format`, Template-Strings (`${var}`,
  `f"..."`, `"..."+"..."`) oder ORM-Methoden, die rohen SQL durchreichen
  (`@Query(value="...", nativeQuery=true)` JPA, `cursor.execute("..." %
  user_input)` Python). Nur parametrisierte Statements (Prepared
  Statements) mit Platzhaltern (`?` JDBC, `@param` ADO.NET, `:name` JPA,
  `$1` PostgreSQL/Go, `%s` Python DB-API) oder geprüfte Stored Procedures
  mit typisierten Parametern. Identifier-Werte (Tabellennamen, Spalten-
  namen, ORDER-BY-Klauseln), die nicht parametrisiert werden können,
  ausschließlich aus Allowlist
  (`if (sortField !in setOf("name", "date")) throw IllegalArgumentException`).
  Beispiele: PostgreSQL `SELECT * FROM users WHERE email = $1` mit
  `pgx.Conn.QueryRow(ctx, sql, email)`; MySQL `SELECT * FROM users
  WHERE id = ?` mit `stmt.setInt(1, id)`; SQL Server
  `SELECT * FROM users WHERE id = @id` mit `cmd.Parameters.Add("@id",
  SqlDbType.Int).Value = id`. Stored Procedures: ein `EXECUTE
  sp_GetUser @userId = ?` mit Parameter, `WITH RECOMPILE` nur wenn
  nötig. Least-Privilege-DB-User pro Anwendung (kein `dbo`, `root`,
  `postgres` superuser); separate Read-/Write-Rollen. SQL-Injection-Test:
  Sleep-Test (`'; WAITFOR DELAY '00:00:05'--`), Boolean-Blind, Time-Based,
  UNION-Based mit `sqlmap` in Pentests. Defensive Tools: SonarQube SQL-
  Injection-Regeln, Semgrep `tainted-sql-string`, GitHub CodeQL
  `cs/sql-injection`, Checkmarx, Veracode. Referenzen: OWASP SQL
  Injection Prevention Cheat Sheet, OWASP ASVS V5.3, CWE-89.
- **EN:** No dynamic SQL strings from user input — never string
  concatenation, `String.format`, template strings (`${var}`, `f"..."`,
  `"..."+"..."`), or ORM methods that pass through raw SQL
  (`@Query(value="...", nativeQuery=true)` JPA, `cursor.execute("..." %
  user_input)` Python). Only parameterised statements (prepared
  statements) with placeholders (`?` JDBC, `@param` ADO.NET, `:name` JPA,
  `$1` PostgreSQL/Go, `%s` Python DB-API) or reviewed stored procedures
  with typed parameters. Identifier values (table names, column names,
  ORDER BY clauses) that cannot be parameterised exclusively from
  allowlist (`if (sortField !in setOf("name", "date")) throw
  IllegalArgumentException`). Examples: PostgreSQL `SELECT * FROM users
  WHERE email = $1` with `pgx.Conn.QueryRow(ctx, sql, email)`; MySQL
  `SELECT * FROM users WHERE id = ?` with `stmt.setInt(1, id)`; SQL
  Server `SELECT * FROM users WHERE id = @id` with
  `cmd.Parameters.Add("@id", SqlDbType.Int).Value = id`. Stored
  procedures: an `EXECUTE sp_GetUser @userId = ?` with parameter, `WITH
  RECOMPILE` only when needed. Least-privilege DB user per application
  (no `dbo`, `root`, `postgres` superuser); separate read/write roles.
  SQL injection test: sleep test (`'; WAITFOR DELAY '00:00:05'--`),
  boolean blind, time-based, UNION-based with `sqlmap` in pentests.
  Defensive tools: SonarQube SQL-injection rules, Semgrep
  `tainted-sql-string`, GitHub CodeQL `cs/sql-injection`, Checkmarx,
  Veracode. References: OWASP SQL Injection Prevention Cheat Sheet,
  OWASP ASVS V5.3, CWE-89.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### Bash / Bash

- **DE:** Variablen immer in doppelten Anführungszeichen (`"$var"`,
  `"${var}"`, `"$@"` statt `$@` oder `$*`); kein `eval` auf nicht
  vertrauenswürdigen Eingaben (Code-Injection-Risiko); `--` als
  Optionsbegrenzer (`rm -- "$file"`, `cd -- "$dir"`) gegen Datei-Namen,
  die mit `-` beginnen; `set -euo pipefail` am Skriptstart (`-e` Exit bei
  Fehler, `-u` Fehler bei undefinierten Variablen, `-o pipefail` Exit-Code
  der gescheiterten Pipeline-Stufe). Zusätzlich `IFS=$'\n\t'` für
  vorhersehbares Word-Splitting. Verwende `[[ ... ]]` (Bash-Builtin) statt
  `[ ... ]` (POSIX) für sicherere Tests; arithmetisch `(( ... ))` statt
  `expr`. Kein Globbing auf User-Input ohne Prüfung (`set -f`/`set +f`
  oder Quoting). `mktemp` für temporäre Dateien (`mktemp -t myapp.XXXXXX`)
  statt `/tmp/$$.tmp`. Heredocs mit Quoting (`<<'EOF'`) gegen Variablen-
  Expansion. Subshells für Side-Effect-Isolation (`( cd dir && cmd )`).
  Externe Befehle: `command -v` für Existenzprüfung; vollständige Pfade
  (`/usr/bin/env`) bei Sicherheits-relevanten Skripten. Statische Analyse:
  `shellcheck` (Pflicht in CI, mindestens Severity warning); `bashate`;
  `shfmt` für Formatierung. Bash-Version: 4+ für `mapfile`, `[[ -v ]]`;
  Bash 5+ für `EPOCHSECONDS`. Beispiel sichere Argumente:
  `if [[ "$1" =~ ^[A-Za-z0-9_-]+$ ]]; then ...`. Verbotene Konstrukte:
  `cmd | sh`, `wget -O- url | bash`, `eval "$user_input"`,
  `unquoted glob *.txt` mit User-Input.
- **EN:** Always quote variables in double quotes (`"$var"`, `"${var}"`,
  `"$@"` instead of `$@` or `$*`); no `eval` on untrusted input (code
  injection risk); `--` as option terminator (`rm -- "$file"`,
  `cd -- "$dir"`) against filenames starting with `-`; `set -euo
  pipefail` at script start (`-e` exit on error, `-u` error on undefined
  variables, `-o pipefail` exit code of failed pipeline stage). Also
  `IFS=$'\n\t'` for predictable word splitting. Use `[[ ... ]]` (bash
  builtin) instead of `[ ... ]` (POSIX) for safer tests; arithmetic
  `(( ... ))` instead of `expr`. No globbing on user input without check
  (`set -f`/`set +f` or quoting). `mktemp` for temporary files
  (`mktemp -t myapp.XXXXXX`) instead of `/tmp/$$.tmp`. Heredocs with
  quoting (`<<'EOF'`) against variable expansion. Subshells for
  side-effect isolation (`( cd dir && cmd )`). External commands:
  `command -v` for existence check; full paths (`/usr/bin/env`) in
  security-relevant scripts. Static analysis: `shellcheck` (mandatory in
  CI, at least severity warning); `bashate`; `shfmt` for formatting.
  Bash version: 4+ for `mapfile`, `[[ -v ]]`; Bash 5+ for
  `EPOCHSECONDS`. Example safe arguments: `if [[ "$1" =~
  ^[A-Za-z0-9_-]+$ ]]; then ...`. Forbidden constructs: `cmd | sh`,
  `wget -O- url | bash`, `eval "$user_input"`, unquoted glob `*.txt`
  with user input.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### PowerShell / PowerShell

- **DE:** `Set-StrictMode -Version Latest` und
  `$ErrorActionPreference = 'Stop'` am Skriptanfang (Erkennung
  undeklarierter Variablen, Eigenschaften und Argumentfehler;
  Fehler nicht stillschweigend ignorieren). Validierte Parameter via
  `[CmdletBinding()]`-Funktion mit Validation-Attributen:
  `[ValidateNotNullOrEmpty()]`, `[ValidateSet('Foo','Bar')]`,
  `[ValidatePattern('^[A-Za-z0-9_-]+$')]`, `[ValidateRange(1,100)]`,
  `[ValidateScript({Test-Path $_})]`. Kein `Invoke-Expression` auf nicht
  vertrauenswürdigen Eingaben (RCE-Risiko); Alternativen: Splatting
  (`& $command @parameters`), `Start-Process -ArgumentList`. PowerShell
  Execution Policy auf Servern mindestens `RemoteSigned`, idealerweise
  `AllSigned` mit Code-Signing-Zertifikat (Authenticode). PowerShell-
  Skripte mit `.psd1`-Manifest und Module-Manifest-Felder. Sichere
  Strings für Geheimnisse: `Read-Host -AsSecureString` und
  `ConvertFrom-SecureString -Key $key`; `[System.Net.NetworkCredential]`
  zur Konvertierung. Web-Anfragen: `Invoke-RestMethod` und
  `Invoke-WebRequest` mit `-UseBasicParsing`,
  `-SkipCertificateCheck` nur in Tests; TLS 1.2+ erzwingen
  (`[Net.ServicePointManager]::SecurityProtocol =
  [Net.SecurityProtocolType]::Tls12 -bor [Net.SecurityProtocolType]::Tls13`).
  PowerShell 7+ (Cross-Platform Core, nicht Windows PowerShell 5.1) für
  neue Skripte. Comment-Based Help (`<#  .SYNOPSIS ... #>`) und
  Pester-Tests (Pflicht für jede Funktion). Statische Analyse:
  `PSScriptAnalyzer` mit Security-Regeln (`Invoke-ScriptAnalyzer -Path
  . -Severity Error,Warning`); `Pester` mit Code-Coverage;
  Inject- und Constrained-Language-Mode bei sensitiven Hosts.
  Logging: `Start-Transcript`, `Write-Verbose`, `Write-Information`
  (kein `Write-Host` für strukturierte Ausgabe). Tipp: `pwsh -NoProfile`
  in CI und Subprozessen, um Profile-Side-Effects zu verhindern.
- **EN:** `Set-StrictMode -Version Latest` and
  `$ErrorActionPreference = 'Stop'` at script start (detection of
  undeclared variables, properties, and argument errors; do not silently
  ignore errors). Validated parameters via `[CmdletBinding()]` function
  with validation attributes: `[ValidateNotNullOrEmpty()]`,
  `[ValidateSet('Foo','Bar')]`,
  `[ValidatePattern('^[A-Za-z0-9_-]+$')]`, `[ValidateRange(1,100)]`,
  `[ValidateScript({Test-Path $_})]`. No `Invoke-Expression` on
  untrusted input (RCE risk); alternatives: splatting (`& $command
  @parameters`), `Start-Process -ArgumentList`. PowerShell execution
  policy on servers at least `RemoteSigned`, ideally `AllSigned` with
  code-signing certificate (Authenticode). PowerShell scripts with
  `.psd1` manifest and module manifest fields. SecureStrings for
  secrets: `Read-Host -AsSecureString` and `ConvertFrom-SecureString
  -Key $key`; `[System.Net.NetworkCredential]` for conversion. Web
  requests: `Invoke-RestMethod` and `Invoke-WebRequest` with
  `-UseBasicParsing`, `-SkipCertificateCheck` only in tests; force TLS
  1.2+ (`[Net.ServicePointManager]::SecurityProtocol =
  [Net.SecurityProtocolType]::Tls12 -bor
  [Net.SecurityProtocolType]::Tls13`). PowerShell 7+ (cross-platform
  Core, not Windows PowerShell 5.1) for new scripts. Comment-based help
  (`<#  .SYNOPSIS ... #>`) and Pester tests (mandatory for every
  function). Static analysis: `PSScriptAnalyzer` with security rules
  (`Invoke-ScriptAnalyzer -Path . -Severity Error,Warning`); `Pester`
  with code coverage; inject and constrained-language mode on sensitive
  hosts. Logging: `Start-Transcript`, `Write-Verbose`,
  `Write-Information` (no `Write-Host` for structured output). Tip:
  `pwsh -NoProfile` in CI and subprocesses to prevent profile side
  effects.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### C / C89 / C / C89

- **DE:** Bounds-Checking bei jedem Array-Zugriff und String-Operation
  (Buffer-Overflows sind die Hauptursache von Memory-Safety-CVEs in
  C/C89). Kein `gets()` (entfernt aus C11; verwende `fgets(buf, sizeof
  buf, stdin)`). Kein ungeprüftes `sprintf()` oder `strcpy()`/`strcat()`
  — Alternativen: `snprintf(buf, sizeof buf, "%s", input)`,
  `strncpy_s`/`strcat_s` (C11 Annex K, optional), `strlcpy`/`strlcat`
  (BSD), oder eigene gepuffte Wrapper mit expliziter Längenprüfung.
  Keine `scanf("%s", buf)` (immer Längenbegrenzung `scanf("%99s", buf)`).
  Integer-Overflows prüfen vor `malloc(n * size)`: `if (n > SIZE_MAX /
  size) abort();`. `free`-Pointer nach Freigabe auf `NULL` setzen
  (`free(p); p = NULL;`); kein Double-Free, kein Use-After-Free.
  CERT-C-Regeln (Top-Verstöße im Review prüfen): STR31-C (sufficient
  buffer), ARR30-C (no out-of-bounds index), INT30-C (unsigned overflow),
  MEM30-C (no use after free), MSC24-C (avoid deprecated functions).
  Statische Analyse Pflicht: `clang-tidy` mit `bugprone-*`,
  `cert-*`-Checks; `cppcheck`; PVS-Studio; Coverity Scan;
  `gcc -Wall -Wextra -Wpedantic -Werror -fanalyzer`. Sanitizer in Tests:
  Address Sanitizer (`-fsanitize=address`), Undefined Behavior Sanitizer
  (`-fsanitize=undefined`), Memory Sanitizer (`-fsanitize=memory`).
  Fuzzing mit `libFuzzer`, AFL++, Honggfuzz. Compiler-Härtung:
  `-D_FORTIFY_SOURCE=3`, `-fstack-protector-strong`, `-fPIE -pie`,
  `-Wl,-z,relro,-z,now`, `-Wl,-z,noexecstack`. Begründung CRA: C ist
  nicht MSL — Begründung in `constitution.md` halten (z. B. cc65/C89-
  Retro-Plattform). Referenzen: SEI CERT C Coding Standard, MISRA C 2023,
  CWE-787 (Out-of-bounds Write), CWE-125 (Out-of-bounds Read), CWE-416
  (Use After Free), CWE-119, ISO/IEC 9899:2018 Annex K.
- **EN:** Bounds checking on every array access and string operation
  (buffer overflows are the main cause of memory-safety CVEs in C/C89).
  No `gets()` (removed from C11; use `fgets(buf, sizeof buf, stdin)`).
  No unchecked `sprintf()` or `strcpy()`/`strcat()` — alternatives:
  `snprintf(buf, sizeof buf, "%s", input)`,
  `strncpy_s`/`strcat_s` (C11 Annex K, optional), `strlcpy`/`strlcat`
  (BSD), or custom buffered wrappers with explicit length check. No
  `scanf("%s", buf)` (always length-limit `scanf("%99s", buf)`). Check
  integer overflows before `malloc(n * size)`: `if (n > SIZE_MAX / size)
  abort();`. Set `free`-pointers to `NULL` after free (`free(p); p =
  NULL;`); no double-free, no use-after-free. CERT C rules (top
  violations to check in review): STR31-C (sufficient buffer), ARR30-C
  (no out-of-bounds index), INT30-C (unsigned overflow), MEM30-C (no use
  after free), MSC24-C (avoid deprecated functions). Mandatory static
  analysis: `clang-tidy` with `bugprone-*`, `cert-*` checks; `cppcheck`;
  PVS-Studio; Coverity Scan; `gcc -Wall -Wextra -Wpedantic -Werror
  -fanalyzer`. Sanitizers in tests: Address Sanitizer
  (`-fsanitize=address`), Undefined Behavior Sanitizer
  (`-fsanitize=undefined`), Memory Sanitizer (`-fsanitize=memory`).
  Fuzzing with `libFuzzer`, AFL++, Honggfuzz. Compiler hardening:
  `-D_FORTIFY_SOURCE=3`, `-fstack-protector-strong`, `-fPIE -pie`,
  `-Wl,-z,relro,-z,now`, `-Wl,-z,noexecstack`. CRA justification: C is
  not MSL — keep justification in `constitution.md` (e.g. cc65/C89 retro
  platform). References: SEI CERT C Coding Standard, MISRA C 2023,
  CWE-787 (Out-of-bounds Write), CWE-125 (Out-of-bounds Read), CWE-416
  (Use After Free), CWE-119, ISO/IEC 9899:2018 Annex K.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 13. Spec-Kit-Secure-Coding-Profile / Spec Kit Secure Coding Profiles

- **DE:** Bei sicherheitsrelevanten Änderungen wird geprüft, ob ein
  sprachspezifisches Secure-Coding-Profil aus dem Spec-Kit-Preset
  `security-governance` ausgefüllt oder gleichwertig abgedeckt ist. Dies
  gilt besonders für Änderungen an Eingabevalidierung, Ausgabe-Codierung,
  Authentifizierung, Autorisierung, Kryptografie, Datei-I/O, Netzwerk-I/O,
  Datenbankzugriff, Logging, Dependency Handling oder Shell-/Script-Code.
  Der Status „Memory-Safe Language" ersetzt diese Prüfung nicht; er
  reduziert nur bestimmte Speicherfehler-Risiken.
- **EN:** For security-relevant changes, check whether a language-specific
  secure-coding profile from the Spec Kit `security-governance` preset has
  been completed or covered in an equivalent way. This applies especially
  to changes in input validation, output encoding, authentication,
  authorisation, cryptography, file I/O, network I/O, database access,
  logging, dependency handling, or shell/script code. "Memory-safe
  language" status does not replace this review; it only reduces certain
  memory-error risks.
- **Akzeptanz / Acceptance:** Pull Request, Review-Dokument oder
  Spec-Kit-Artefakt verweist auf die ausgefüllten Profile für die
  betroffenen Sprachen (z. B. C/C89, C#/.NET, Rust, Go, Swift,
  Java/Kotlin, Python, TypeScript/JavaScript, SQL, Bash, PowerShell) oder
  begründet die Nichtanwendbarkeit. / Pull request, review document, or
  Spec Kit artefact links the completed profiles for affected languages
  (for example C/C89, C#/.NET, Rust, Go, Swift, Java/Kotlin, Python,
  TypeScript/JavaScript, SQL, Bash, PowerShell) or justifies
  non-applicability.
- **Referenz / Reference:** Spec-Kit `security-governance`, Template
  `secure-coding-language-rules-template`.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Review, PR, Spec-Kit-Artefakt oder N/A-Begründung nennen. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name review, PR, Spec Kit artefact, or N/A rationale. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

### Abschluss / Closure

**DE:** Reviewer trägt Datum, Name und Ergebnis (akzeptiert / Änderungen
nötig) ein. Bei „Änderungen nötig" werden konkrete Punkte als Kommentar
am Pull Request hinterlegt.

**EN:** Reviewer records date, name, and result (accepted / changes
needed). For "changes needed", concrete points are left as comments on the
pull request.

### Glossar / Glossary

**DE:** Dieses Glossar erklärt die wichtigsten Begriffe dieser Checkliste in Alltagssprache. Es ändert keine Anforderungen, sondern macht die vorhandenen Prüfpunkte leichter verständlich.

**EN:** This glossary explains the most important terms in this checklist in plain language. It does not change requirements; it makes the existing review items easier to understand.

<a id="cl-08-glossar-code-review"></a>

#### Code Review

- **DE:** Ein Code Review ist die Prüfung von Änderungen durch andere Personen. Beim Sicherheits-Code-Review wird besonders auf Risiken, Eingaben, Berechtigungen, Fehlerbehandlung und Abhängigkeiten geachtet.
- **EN:** A code review is the review of changes by other people. Security code review focuses especially on risks, inputs, permissions, error handling, and dependencies.

<a id="cl-08-glossar-input-validation"></a>

#### Eingabevalidierung / Input Validation

- **DE:** Eingabevalidierung prüft, ob Daten das erwartete Format, die erlaubte Länge und den erlaubten Wertebereich haben. Sie schützt vor fehlerhaften oder gefährlichen Eingaben.
- **EN:** Input validation checks whether data has the expected format, allowed length, and allowed value range. It protects against wrong or dangerous input.

<a id="cl-08-glossar-output-encoding"></a>

#### Ausgabe-Codierung / Output Encoding

- **DE:** Ausgabe-Codierung stellt Daten passend für den Zielkontext dar, zum Beispiel HTML, JSON oder SQL. Sie verhindert, dass Daten versehentlich als Code ausgeführt werden.
- **EN:** Output encoding represents data correctly for the target context, for example HTML, JSON, or SQL. It prevents data from accidentally being executed as code.

<a id="cl-08-glossar-authentication"></a>

#### Authentifizierung / Authentication

- **DE:** Authentifizierung prüft, wer jemand ist oder welcher Dienst sich anmeldet. Beispiele sind Passwort, Zertifikat, Token, MFA oder Single Sign-on.
- **EN:** Authentication checks who a person is or which service signs in. Examples are password, certificate, token, MFA, or single sign-on.

<a id="cl-08-glossar-authorisation"></a>

#### Autorisierung / Authorisation

- **DE:** Autorisierung entscheidet, was eine angemeldete Person oder ein Dienst tun darf. Sie muss serverseitig geprüft werden und folgt nach der Authentifizierung.
- **EN:** Authorisation decides what an authenticated person or service may do. It must be checked server-side and happens after authentication.

<a id="cl-08-glossar-session-management"></a>

#### Sitzungsverwaltung / Session Management

- **DE:** Sitzungsverwaltung steuert angemeldete Sitzungen, zum Beispiel Cookies, Ablaufzeiten, Abmeldung und Schutz gegen Session-Übernahme.
- **EN:** Session management controls signed-in sessions, for example cookies, expiry times, logout, and protection against session takeover.

<a id="cl-08-glossar-secret-store"></a>

#### Secret Store

- **DE:** Ein Secret Store speichert Geheimnisse wie Passwörter, API-Schlüssel oder Tokens geschützt. Geheimnisse sollen nicht im Code, in Logs oder in Tickets stehen.
- **EN:** A secret store protects secrets such as passwords, API keys, or tokens. Secrets should not be in code, logs, or tickets.

<a id="cl-08-glossar-logging-audit"></a>

#### Logging und Audit / Logging and Audit

- **DE:** Logging zeichnet wichtige Ereignisse auf. Audit-Fähigkeit bedeutet, dass diese Aufzeichnungen verständlich, geschützt und für Prüfungen nutzbar sind.
- **EN:** Logging records important events. Audit capability means these records are understandable, protected, and usable for reviews.

<a id="cl-08-glossar-dependency"></a>

#### Abhängigkeit / Dependency

- **DE:** Eine Abhängigkeit ist fremder Code oder ein Paket, das ein Projekt nutzt. Abhängigkeiten brauchen Pflege, Lizenzprüfung und Schwachstellenprüfung.
- **EN:** A dependency is third-party code or a package used by a project. Dependencies need maintenance, licence review, and vulnerability checks.

<a id="cl-08-glossar-sast"></a>

#### SAST

- **DE:** SAST ist statische Codeanalyse. Der Code wird untersucht, ohne dass das Programm läuft, zum Beispiel auf unsichere Funktionen oder Datenflüsse.
- **EN:** SAST is static code analysis. The code is inspected without running the program, for example for unsafe functions or data flows.

<a id="cl-08-glossar-dast"></a>

#### DAST

- **DE:** DAST ist dynamische Anwendungssicherheitsprüfung. Die laufende Anwendung wird von außen getestet, ähnlich wie ein Angreifer sie sehen würde.
- **EN:** DAST is dynamic application security testing. The running application is tested from the outside, similar to how an attacker would see it.

<a id="cl-08-glossar-linter"></a>

#### Linter

- **DE:** Ein Linter prüft Quellcode automatisch gegen Regeln. Das können Stilregeln, mögliche Fehler oder Sicherheitsregeln sein.
- **EN:** A linter checks source code automatically against rules. These can be style rules, possible bugs, or security rules.

<a id="cl-08-glossar-codeql"></a>

#### CodeQL

- **DE:** CodeQL ist ein Werkzeug für statische Analyse. Es sucht Muster und Datenflüsse im Code, die auf Sicherheitsprobleme hinweisen können.
- **EN:** CodeQL is a static analysis tool. It searches code patterns and data flows that may indicate security issues.

<a id="cl-08-glossar-semgrep"></a>

#### Semgrep

- **DE:** Semgrep ist ein Werkzeug für statische Analyse mit gut lesbaren Regeln. Teams können eigene Regeln für sichere Codierung ergänzen.
- **EN:** Semgrep is a static analysis tool with readable rules. Teams can add their own rules for secure coding.

<a id="cl-08-glossar-sonarqube"></a>

#### SonarQube

- **DE:** SonarQube prüft Codequalität und Sicherheitsaspekte. Ergebnisse müssen bewertet werden, weil nicht jeder Fund automatisch ein echter Fehler ist.
- **EN:** SonarQube checks code quality and security aspects. Results must be assessed because not every finding is automatically a real defect.

<a id="cl-08-glossar-xss"></a>

#### XSS / Cross-Site Scripting

- **DE:** XSS ist ein Angriff, bei dem fremder Skriptcode im Browser anderer Personen ausgeführt wird. Häufige Ursachen sind fehlende Ausgabe-Codierung und ungeprüfte Eingaben.
- **EN:** XSS is an attack where foreign script code runs in other people’s browsers. Common causes are missing output encoding and unchecked input.

<a id="cl-08-glossar-sql-injection"></a>

#### SQL Injection

- **DE:** SQL Injection ist ein Angriff, bei dem Eingaben Datenbankbefehle verändern. Schutz bieten parametrisierte Abfragen und gute Eingabeprüfung.
- **EN:** SQL injection is an attack where input changes database commands. Protection includes parameterised queries and good input checks.

<a id="cl-08-glossar-path-traversal"></a>

#### Path Traversal

- **DE:** Path Traversal ist ein Angriff auf Dateipfade. Angreifende versuchen, auf Dateien außerhalb des erlaubten Bereichs zuzugreifen.
- **EN:** Path traversal is an attack on file paths. Attackers try to access files outside the allowed area.

<a id="cl-08-glossar-spec-kit"></a>

#### Spec Kit

- **DE:** Spec Kit ist ein werkzeuggestützter Ablauf für spezifikationsgetriebene Entwicklung. Es erzeugt und nutzt Markdown-Artefakte wie Spezifikation, Plan, Aufgaben und Analysen.
- **EN:** Spec Kit is a tool-supported flow for specification-driven development. It creates and uses Markdown artefacts such as specification, plan, tasks, and analyses.

### Versionshistorie / Version History

- **Version 1.0 (2026-04-27):** Erstfassung / Initial version
- **Version 1.1 (2026-04-27):** Erweiterte Durchführungshinweise, Quellen-URLs, Statusfelder und Beispiele / Extended guidance, source URLs, status fields, and examples
- **Version 1.2 (2026-06-15):** Prüfpunkt 13 für Spec-Kit-Secure-Coding-Profile ergänzt; synchron mit Richtlinie Sichere Entwicklung v2.9.0. / Added item 13 for Spec Kit secure-coding profiles; synchronized with Richtlinie Sichere Entwicklung v2.9.0.

- **Version 1.3 (2026-06-16):** Verständlichkeit der Durchführungshinweise, Begründungs-, Evidenz- und Maßnahmenfelder für Entwickler:innen und Auszubildende präzisiert; CEFR-B2- und WCAG-2.2-AA-konforme Ausfüllhilfe ergänzt. / Refined understandability of implementation guidance, rationale, evidence, and action fields for developers and apprentices; added CEFR B2 and WCAG 2.2 AA conformant completion help.

- **Version 1.4 (2026-06-17):** Glossar und Begriff-Links für Entwickler:innen und Fachinformatik-Auszubildende ergänzt; wichtige Abkürzungen und Technologien in CEFR-B2-Sprache erklärt. / Added glossary and term links for developers and IT specialist apprentices; explained important abbreviations and technologies in CEFR B2 language.
- **Version 1.5 (2026-06-17):** Test-KPIs an Richtlinie Sichere Entwicklung v2.10.0 angepasst: sicherheitskritische Module mindestens 85 % Branch Coverage; Integrationstest-Abdeckung öffentlicher Schnittstellen und kritischer UI-Flows mindestens 80 %. / Aligned test KPIs with Secure Development Guideline v2.10.0: security-critical modules at least 85 % branch coverage; integration-test coverage of public interfaces and critical UI flows at least 80 %.

---
