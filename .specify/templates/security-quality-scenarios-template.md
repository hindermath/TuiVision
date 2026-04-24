# Sicherheits-Qualitätsszenarien / Security Quality Scenarios: [PROJECT]

**Projekt / Project**: [Level-2 project name]
**Erstellt / Created**: [YYYY-MM-DD]
**Letzte Aktualisierung / Last Updated**: [YYYY-MM-DD]
**Constitution-Referenz / Constitution Reference**: Principle XII (A.8.28), Principle XIII (A.8.27)
**iSAQB-Referenz / iSAQB Reference**: CPSA-F — Quality Attribute Scenarios

<!--
  Dieses Dokument beschreibt messbare Sicherheits-Qualitätsszenarien nach
  iSAQB CPSA-F-Methodik. Jedes Szenario definiert Stimulus, Umgebung und
  erwartete Reaktion, um Sicherheitsanforderungen testbar zu machen.

  This document describes measurable security quality scenarios following
  iSAQB CPSA-F methodology. Each scenario defines stimulus, environment,
  and expected response to make security requirements testable.
-->

## Szenario-Format / Scenario Format

<!--
  Jedes Szenario folgt dem iSAQB-Qualitätsszenario-Schema:
  - Quelle / Source: Wer oder was löst das Szenario aus
  - Stimulus: Was passiert (Ereignis, Anfrage, Angriff)
  - Artefakt / Artifact: Welche Komponente ist betroffen
  - Umgebung / Environment: Unter welchen Bedingungen (Produktion, Last, etc.)
  - Antwort / Response: Was soll das System tun
  - Antwortmetrik / Response Measure: Wie wird die Antwort gemessen

  Each scenario follows the iSAQB quality scenario schema:
  - Source: Who or what triggers the scenario
  - Stimulus: What happens (event, request, attack)
  - Artifact: Which component is affected
  - Environment: Under which conditions (production, load, etc.)
  - Response: What the system should do
  - Response Measure: How the response is measured
-->

---

## 1. Authentifizierung / Authentication

### QS-SEC-001: Brute-Force-Schutz / Brute Force Protection

| Element | Beschreibung / Description |
|---|---|
| **Quelle / Source** | Nicht authentifizierter Benutzer / Unauthenticated user |
| **Stimulus** | 10 fehlgeschlagene Login-Versuche innerhalb von 5 Minuten / 10 failed login attempts within 5 minutes |
| **Artefakt / Artifact** | Authentifizierungs-Endpunkt / Authentication endpoint |
| **Umgebung / Environment** | Produktion, normale Last / Production, normal load |
| **Antwort / Response** | Konto wird temporär gesperrt, Vorfall wird geloggt / Account is temporarily locked, incident is logged |
| **Antwortmetrik / Response Measure** | Sperre aktiv nach <= 10 Versuchen; Entsperrung nach [N] Minuten; Log-Eintrag vorhanden / Lock active after <= 10 attempts; unlock after [N] minutes; log entry present |

### QS-SEC-002: Token-Ablauf / Token Expiry

| Element | Beschreibung / Description |
|---|---|
| **Quelle / Source** | Authentifizierter Benutzer / Authenticated user |
| **Stimulus** | Zugriff mit abgelaufenem Token / Access with expired token |
| **Artefakt / Artifact** | API-Middleware / API middleware |
| **Umgebung / Environment** | Produktion / Production |
| **Antwort / Response** | Anfrage wird mit 401 abgelehnt, kein Datenzugriff / Request rejected with 401, no data access |
| **Antwortmetrik / Response Measure** | 100 % der Anfragen mit abgelaufenem Token werden abgelehnt; Antwortzeit < 100 ms / 100% of requests with expired tokens rejected; response time < 100ms |

---

## 2. Autorisierung / Authorization

### QS-SEC-003: Horizontale Rechteausweitung / Horizontal Privilege Escalation

| Element | Beschreibung / Description |
|---|---|
| **Quelle / Source** | Authentifizierter Benutzer mit Rolle "User" / Authenticated user with role "User" |
| **Stimulus** | Zugriff auf Ressource eines anderen Benutzers / Access to another user's resource |
| **Artefakt / Artifact** | Autorisierungs-Middleware / Authorization middleware |
| **Umgebung / Environment** | Produktion / Production |
| **Antwort / Response** | Zugriff verweigert (403), Vorfall geloggt / Access denied (403), incident logged |
| **Antwortmetrik / Response Measure** | 0 erfolgreiche horizontale Zugriffe; Log-Eintrag mit Benutzer-ID und Ressource / 0 successful horizontal accesses; log entry with user ID and resource |

### QS-SEC-004: Vertikale Rechteausweitung / Vertical Privilege Escalation

| Element | Beschreibung / Description |
|---|---|
| **Quelle / Source** | Authentifizierter Benutzer mit Rolle "User" / Authenticated user with role "User" |
| **Stimulus** | Zugriff auf Admin-Endpunkt / Access to admin endpoint |
| **Artefakt / Artifact** | Autorisierungs-Middleware / Authorization middleware |
| **Umgebung / Environment** | Produktion / Production |
| **Antwort / Response** | Zugriff verweigert (403), Vorfall geloggt / Access denied (403), incident logged |
| **Antwortmetrik / Response Measure** | 0 erfolgreiche vertikale Zugriffe; Antwort enthält keine Admin-Daten / 0 successful vertical accesses; response contains no admin data |

---

## 3. Eingabevalidierung / Input Validation

### QS-SEC-005: SQL-Injection-Abwehr / SQL Injection Defense

| Element | Beschreibung / Description |
|---|---|
| **Quelle / Source** | Externer Benutzer / External user |
| **Stimulus** | Eingabe enthält SQL-Injection-Payload (`' OR 1=1 --`) / Input contains SQL injection payload |
| **Artefakt / Artifact** | Datenbank-Zugriffsschicht / Data access layer |
| **Umgebung / Environment** | Produktion / Production |
| **Antwort / Response** | Eingabe wird als Literal behandelt, kein SQL ausgeführt / Input treated as literal, no SQL executed |
| **Antwortmetrik / Response Measure** | 0 erfolgreiche Injections; parametrisierte Queries in 100 % der DB-Zugriffe / 0 successful injections; parameterized queries in 100% of DB accesses |

### QS-SEC-006: XSS-Abwehr / XSS Defense

| Element | Beschreibung / Description |
|---|---|
| **Quelle / Source** | Externer Benutzer / External user |
| **Stimulus** | Eingabe enthält Script-Tag (`<script>alert(1)</script>`) / Input contains script tag |
| **Artefakt / Artifact** | Ausgabe-Rendering / Output rendering |
| **Umgebung / Environment** | Produktion / Production |
| **Antwort / Response** | Script wird HTML-encoded ausgegeben, nicht ausgeführt / Script is HTML-encoded in output, not executed |
| **Antwortmetrik / Response Measure** | 0 ausführbare Scripts in der Ausgabe; Output-Encoding in 100 % der Ausgabepunkte / 0 executable scripts in output; output encoding in 100% of output points |

### QS-SEC-007: Command-Injection-Abwehr / Command Injection Defense

| Element | Beschreibung / Description |
|---|---|
| **Quelle / Source** | Externer Benutzer oder API-Consumer / External user or API consumer |
| **Stimulus** | Eingabe enthält Shell-Metazeichen (`; rm -rf /`) / Input contains shell metacharacters |
| **Artefakt / Artifact** | Shell-Aufruf-Schicht / Shell invocation layer |
| **Umgebung / Environment** | Produktion / Production |
| **Antwort / Response** | Metazeichen werden escaped oder Eingabe wird abgelehnt / Metacharacters escaped or input rejected |
| **Antwortmetrik / Response Measure** | 0 erfolgreiche Command-Injections; alle externen Eingaben vor Shell-Aufruf validiert / 0 successful command injections; all external inputs validated before shell invocation |

---

## 4. Verschlüsselung / Encryption

### QS-SEC-008: TLS-Durchsetzung / TLS Enforcement

| Element | Beschreibung / Description |
|---|---|
| **Quelle / Source** | Client (Browser, API-Consumer) |
| **Stimulus** | HTTP-Anfrage (nicht HTTPS) an Produktions-Endpunkt / HTTP request (not HTTPS) to production endpoint |
| **Artefakt / Artifact** | Webserver / Reverse Proxy |
| **Umgebung / Environment** | Produktion / Production |
| **Antwort / Response** | Redirect zu HTTPS (301) oder Ablehnung / Redirect to HTTPS (301) or rejection |
| **Antwortmetrik / Response Measure** | 0 unverschlüsselte Verbindungen in Produktion; TLS 1.2+ erzwungen / 0 unencrypted connections in production; TLS 1.2+ enforced |

### QS-SEC-009: Verschlüsselung ruhender Daten / Encryption at Rest

| Element | Beschreibung / Description |
|---|---|
| **Quelle / Source** | Datenbank-Administrator oder Angreifer mit Festplatten-Zugriff / DBA or attacker with disk access |
| **Stimulus** | Direkter Zugriff auf Datenbankdateien / Direct access to database files |
| **Artefakt / Artifact** | Datenbank / Database |
| **Umgebung / Environment** | Produktion / Production |
| **Antwort / Response** | Sensible Daten sind verschlüsselt, nicht im Klartext lesbar / Sensitive data is encrypted, not readable in plaintext |
| **Antwortmetrik / Response Measure** | AES-256 oder gleichwertig für alle sensiblen Felder; Schlüssel in Key Vault / AES-256 or equivalent for all sensitive fields; keys in key vault |

---

## 5. Fehlerbehandlung / Error Handling

### QS-SEC-010: Informationspreisgabe bei Fehlern / Information Disclosure on Errors

| Element | Beschreibung / Description |
|---|---|
| **Quelle / Source** | Externer Benutzer / External user |
| **Stimulus** | Anfrage verursacht unbehandelte Ausnahme / Request causes unhandled exception |
| **Artefakt / Artifact** | Fehlerbehandlungs-Middleware / Error handling middleware |
| **Umgebung / Environment** | Produktion / Production |
| **Antwort / Response** | Generische Fehlermeldung an Benutzer, detaillierter Log intern / Generic error message to user, detailed log internally |
| **Antwortmetrik / Response Measure** | 0 Stack-Traces in Produktions-Antworten; 0 Connection-Strings exponiert; Korrelations-ID in Antwort und Log / 0 stack traces in production responses; 0 connection strings exposed; correlation ID in response and log |

### QS-SEC-011: Fail-Safe-Default

| Element | Beschreibung / Description |
|---|---|
| **Quelle / Source** | System (Konfigurationsfehler oder Ausfall einer Abhängigkeit) / System (misconfiguration or dependency failure) |
| **Stimulus** | Autorisierungsdienst nicht erreichbar / Authorization service unavailable |
| **Artefakt / Artifact** | Autorisierungs-Middleware / Authorization middleware |
| **Umgebung / Environment** | Produktion, Teilausfall / Production, partial outage |
| **Antwort / Response** | Alle Anfragen werden verweigert (Deny by Default) / All requests denied (deny by default) |
| **Antwortmetrik / Response Measure** | 0 erlaubte Anfragen während Autorisierungsausfall; System kehrt nach Wiederherstellung zum Normalbetrieb zurück / 0 allowed requests during authorization outage; system returns to normal after recovery |

---

## 6. Logging und Auditierung / Logging and Auditing

### QS-SEC-012: Sicherheitsrelevante Ereignisse / Security-Relevant Events

| Element | Beschreibung / Description |
|---|---|
| **Quelle / Source** | System / System |
| **Stimulus** | Sicherheitsrelevantes Ereignis tritt ein (Login, fehlgeschlagener Zugriff, Konfigurationsänderung) / Security-relevant event occurs (login, failed access, config change) |
| **Artefakt / Artifact** | Logging-Infrastruktur / Logging infrastructure |
| **Umgebung / Environment** | Alle Umgebungen / All environments |
| **Antwort / Response** | Ereignis wird mit Zeitstempel, Benutzer-ID, Korrelations-ID geloggt / Event logged with timestamp, user ID, correlation ID |
| **Antwortmetrik / Response Measure** | 100 % der definierten Sicherheitsereignisse geloggt; keine Secrets in Logs; Logs tamper-evident / 100% of defined security events logged; no secrets in logs; logs tamper-evident |

### QS-SEC-013: Keine Secrets in Logs / No Secrets in Logs

| Element | Beschreibung / Description |
|---|---|
| **Quelle / Source** | Entwickler oder automatisierter Prozess / Developer or automated process |
| **Stimulus** | Log-Statement wird geschrieben / Log statement is written |
| **Artefakt / Artifact** | Logging-Framework / Logging framework |
| **Umgebung / Environment** | Alle Umgebungen / All environments |
| **Antwort / Response** | Keine Passwörter, Tokens, API-Keys oder PII in Log-Ausgabe / No passwords, tokens, API keys, or PII in log output |
| **Antwortmetrik / Response Measure** | 0 Secret-Patterns in Log-Dateien (automatisiert prüfbar) / 0 secret patterns in log files (automated check possible) |

---

## 7. Abhängigkeiten / Dependencies

### QS-SEC-014: Schwachstellenfreie Abhängigkeiten / Vulnerability-Free Dependencies

| Element | Beschreibung / Description |
|---|---|
| **Quelle / Source** | CI/CD-Pipeline |
| **Stimulus** | Build oder Release wird ausgelöst / Build or release triggered |
| **Artefakt / Artifact** | Abhängigkeitsbaum / Dependency tree |
| **Umgebung / Environment** | CI/CD / CI/CD |
| **Antwort / Response** | Automatischer Audit-Scan, Build schlägt bei kritischen CVEs fehl / Automatic audit scan, build fails on critical CVEs |
| **Antwortmetrik / Response Measure** | 0 kritische CVEs in Release-Builds; Audit-Bericht als Build-Artefakt / 0 critical CVEs in release builds; audit report as build artifact |

---

## Szenario-Übersicht / Scenario Overview

| ID | Kategorie / Category | Szenario / Scenario | Priorität / Priority | Status |
|---|---|---|---|---|
| QS-SEC-001 | Authentifizierung | Brute-Force-Schutz | [P1/P2/P3] | [Offen/Implementiert/Verifiziert] |
| QS-SEC-002 | Authentifizierung | Token-Ablauf | [P1/P2/P3] | [Offen/Implementiert/Verifiziert] |
| QS-SEC-003 | Autorisierung | Horizontale Rechteausweitung | [P1/P2/P3] | [Offen/Implementiert/Verifiziert] |
| QS-SEC-004 | Autorisierung | Vertikale Rechteausweitung | [P1/P2/P3] | [Offen/Implementiert/Verifiziert] |
| QS-SEC-005 | Eingabevalidierung | SQL-Injection | [P1/P2/P3] | [Offen/Implementiert/Verifiziert] |
| QS-SEC-006 | Eingabevalidierung | XSS | [P1/P2/P3] | [Offen/Implementiert/Verifiziert] |
| QS-SEC-007 | Eingabevalidierung | Command-Injection | [P1/P2/P3] | [Offen/Implementiert/Verifiziert] |
| QS-SEC-008 | Verschlüsselung | TLS-Durchsetzung | [P1/P2/P3] | [Offen/Implementiert/Verifiziert] |
| QS-SEC-009 | Verschlüsselung | Encryption at Rest | [P1/P2/P3] | [Offen/Implementiert/Verifiziert] |
| QS-SEC-010 | Fehlerbehandlung | Informationspreisgabe | [P1/P2/P3] | [Offen/Implementiert/Verifiziert] |
| QS-SEC-011 | Fehlerbehandlung | Fail-Safe-Default | [P1/P2/P3] | [Offen/Implementiert/Verifiziert] |
| QS-SEC-012 | Logging | Sicherheitsereignisse | [P1/P2/P3] | [Offen/Implementiert/Verifiziert] |
| QS-SEC-013 | Logging | Keine Secrets in Logs | [P1/P2/P3] | [Offen/Implementiert/Verifiziert] |
| QS-SEC-014 | Abhängigkeiten | Schwachstellenfreie Abhängigkeiten | [P1/P2/P3] | [Offen/Implementiert/Verifiziert] |

## Projektspezifische Szenarien / Project-Specific Scenarios

<!--
  Hier projektspezifische Szenarien ergänzen, die über die Standard-Szenarien
  hinausgehen. Format wie oben beibehalten.

  Add project-specific scenarios here that go beyond the standard scenarios.
  Keep the format as above.
-->

### QS-SEC-P001: [Projektspezifisches Szenario / Project-Specific Scenario]

| Element | Beschreibung / Description |
|---|---|
| **Quelle / Source** | |
| **Stimulus** | |
| **Artefakt / Artifact** | |
| **Umgebung / Environment** | |
| **Antwort / Response** | |
| **Antwortmetrik / Response Measure** | |

## Referenzen / References

- Constitution Principle XII: Secure Code Generation (ISO 27002:2022 A.8.28)
- Constitution Principle XIII: Secure Software Architecture (ISO 27002:2022 A.8.27)
- ISO/IEC 25010:2023: Security as Quality Attribute
- iSAQB CPSA-F Curriculum: Quality Attribute Scenarios
- iSAQB Module WEBSEC: Web Security Patterns
- arc42: Quality Requirements (Section 10)

<!-- EN: security-quality-scenarios-template.md
[DE-Zusammenfassung: Template fuer messbare Sicherheits-Qualitaetsszenarien nach iSAQB CPSA-F-Methodik, abgeleitet aus Constitution Principle XII und XIII.]
-->
